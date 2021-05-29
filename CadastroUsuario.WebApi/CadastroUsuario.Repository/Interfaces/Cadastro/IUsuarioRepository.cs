using CadastroUsuario.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace CadastroUsuario.Repository.Interfaces.Cadastro
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        /// <summary>
        /// Lista todos usuarios ativos cadastrados
        /// </summary>
        /// <returns></returns>
        List<Usuario> GetUsuariosAtivos();
    }
}
