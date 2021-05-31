using CadastroUsuario.Domain.Entities.Cadastro;
using CadastroUsuario.Repository.Context;
using CadastroUsuario.Repository.Interfaces.Cadastro;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuario.Repository.Repositories.Cadastro
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(CadastroUsuarioContext context) : base(context)
        { }


        /// <summary>
        /// Lista todos usuarios ativos cadastrados
        /// </summary>
        /// <returns></returns>
        public List<Usuario> GetUsuariosAtivos()
        {
            using (var db = new CadastroUsuarioContext())
            {
                var lQuery = (from Usuario in db.Usuario.AsNoTracking()
                              where Usuario.Ativo == true
                              orderby Usuario.Nome
                              select Usuario)
                              .ToList();

                return lQuery;
            }
        }
    }
}
