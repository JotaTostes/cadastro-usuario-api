using CadastroUsuario.Domain.Entities.Cadastro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CadastroUsuario.Repository.Context
{
    public class CadastroUsuarioContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public CadastroUsuarioContext()
        {}


        public CadastroUsuarioContext(DbContextOptions<CadastroUsuarioContext> options) : base(options)
        {
            Database.SetCommandTimeout(180);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static readonly ILoggerFactory consoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });

        /// <summary>
        /// Recupera a conexão com banco de dados
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder
                .UseLoggerFactory(consoleLoggerFactory)
                .UseSqlServer(config.GetSection("ConnectionStrings")["DefaultConnection"], b => b.EnableRetryOnFailure(5,TimeSpan.FromSeconds(10),null));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
