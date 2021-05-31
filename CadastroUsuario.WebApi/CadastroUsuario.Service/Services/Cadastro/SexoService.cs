using CadastroUsuario.DataVo.ValueObjects.Cadastro;
using CadastroUsuario.Domain.Entities.Cadastro;
using CadastroUsuario.Repository.Interfaces.Cadastro;
using CadastroUsuario.Service.Interfaces;
using CadastroUsuario.Service.Interfaces.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Service.Services.Cadastro
{
    public class SexoService : ISexoService
    {
        private readonly ISexoRepository _sexoRepository;

        public SexoService(ISexoRepository sexoRepository)
        {
            _sexoRepository = sexoRepository;
        }

        /// <summary>
        /// Retorna todos sexo
        /// </summary>
        /// <returns></returns>
        public List<Sexo> GetAll() =>
            _sexoRepository.GetAll();
       
    }
}
