using CadastroUsuario.Domain.Entities.Cadastro;
using CadastroUsuario.Repository.Context;
using CadastroUsuario.Repository.Interfaces.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Repository.Repositories.Cadastro
{
    public class SexoRepository : BaseRepository<Sexo>, ISexoRepository
    {
        public SexoRepository(CadastroUsuarioContext context) : base(context)
        {}
    }
}
