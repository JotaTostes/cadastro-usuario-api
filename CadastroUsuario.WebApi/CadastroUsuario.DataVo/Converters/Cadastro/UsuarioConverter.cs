using CadastroUsuario.DataVo.Interfaces;
using CadastroUsuario.DataVo.ValueObjects.Cadastro;
using CadastroUsuario.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroUsuario.DataVo.Converters.Cadastro
{
    public class UsuarioConverter : IParser<UsuarioVo, Usuario>, IParser<Usuario, UsuarioVo>
    {

        public Usuario Parse(UsuarioVo origin)
        {
            if (origin == null) return null;
            return new Usuario
            {
                UsuarioId = origin.UsuarioId,
                DataNascimento = origin.DataNascimento,
                Email = origin.Email,
                Nome = origin.Nome,
                Senha = origin.Senha,
                Sexo = origin.Sexo,
                Ativo = origin.Ativo
            };
        }

        public UsuarioVo Parse(Usuario origin)
        {
            if (origin == null) return null;
            return new UsuarioVo
            {
                UsuarioId = origin.UsuarioId,
                DataNascimento = origin.DataNascimento,
                Email = origin.Email,
                Nome = origin.Nome,
                Senha = origin.Senha,
                Sexo = origin.Sexo,
                Ativo = origin.Ativo
            };
        }

        public List<Usuario> ParseList(List<UsuarioVo> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UsuarioVo> ParseList(List<Usuario> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
