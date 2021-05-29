using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using CadastroUsuario.Ioc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CadastroUsuario.InfraStructure.Security;
using CadastroUsuario.Domain.Security;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.AspNetCore.Rewrite;
using CadastroUsuario.Repository.Context;

namespace CadastroUsuario.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "CorsPolicy";


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMvc().AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    builder
                    .AllowCredentials()
                    .SetIsOriginAllowed(hostName => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                // handle loops correctly
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // use standard name conversion of properties
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                // include $id property in the output
                //options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            });

            // Set string connection
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CadastroUsuarioContext>(options =>
                  options.UseSqlServer(
                      sqlConnectionString,
                      b => b.MigrationsAssembly("CadastroUsuario.WebApi")
                  )
              );

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Documentação de Apis",
                        Version = "v1.0",
                        Description = "# Introducão\nSeja bem-vindo a documentação da API de Cadastro de Usuarios!\n"
                                      + "Esta API foi criada utilizando o padrão REST que possibilita a integração de seu sistema ao nosso, "
                                      + "sendo assim você também pode extender ou recriar as funcionalidades existentes na nossa plataforma, tudo isso consumindo a API que está documentada abaixo.\n\n\n"
                                      + "# Como usar a API?\nLogo a seguir você encontrará todos os recursos e metódos suportados pela API, sendo que essa página possibilita que você teste os recursos "
                                      + "e métodos diretamente através dela.\n\n\n# Autenticação\nVocê precisará de um Usuário, Senha e uma chave de API (API Key) para identificar a conta que está realizando solicitações para a API. \n"
                                      + "Para isso você deverá solicitar para nossa equipe previamente.",
                        Contact = new OpenApiContact
                        {
                            Name = "João Guilherme Tostes"
                        }
                    });

                c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "Por favor, insira no campo a palavra 'Bearer' espaço JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = "CadastroUsuario.WebApi" + ".XML";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                //c.IncludeXmlComments(commentsFile);
            });

            services.AddMvcCore(options => options.EnableEndpointRouting = false);

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;

                    options.DefaultApiVersion = new ApiVersion(new DateTime(2021, 5, 27));

                    // automatically applies an api version based on the name of the defining controller's namespace
                    options.Conventions.Add(new VersionByNamespaceConvention());
                });

            services.AddAuthentication();
            services.AddMvc().AddWebApiConventions();

            //InjecaoDependenciaServicos(ref services);

            services.AddSingleton<IConfiguration>(Configuration);
            DependencyInjection.InjecaoDependenciaRepositorios(ref services);
            DependencyInjection.InjecaoDependenciaServicos(ref services);
            DependencyInjection.InjecaoDependenciaConverters(ref services);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var defaultLang = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;
                    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                }));
            });

            services.AddControllers();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[] {
                new CultureInfo("en-US")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api de integração - Versão 1.0");

                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseMvc(routes => {
                routes.MapRoute(name: "DefaultApi", template: "{controller=Values}/{id?}");
            });

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
