using CadastroUsuario.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Service.Interfaces.Cadastro
{
    public interface ISexoService
    {
        /// <summary>
        /// Retorna todos sexo
        /// </summary>
        /// <returns></returns>
        List<Sexo> GetAll();
    }
}
