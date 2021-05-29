using CadastroUsuario.DataVo.Converters.Cadastro;
using CadastroUsuario.Repository.Interfaces.Cadastro;
using CadastroUsuario.Repository.Repositories.Cadastro;
using CadastroUsuario.Service.Cadastro;
using CadastroUsuario.Service.Interfaces.Cadastro;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Ioc
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adiciona a injeção de dependência entre os serviços e suas interfaces.
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaServicos(ref IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();

        }

        /// <summary>
        /// Adiciona a injeção de dependência entre os repositorios e suas interfaces.
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaRepositorios(ref IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        }

        /// <summary>
        /// Adiciona a injeção de dependência entre os converters
        /// </summary>
        /// <param name="services"></param>
        public static void InjecaoDependenciaConverters(ref IServiceCollection services)
        {
            services.AddScoped<UsuarioConverter, UsuarioConverter>();

        }
    }
}
