using CadastroUsuario.DataVo.Converters.Cadastro;
using CadastroUsuario.DataVo.ValueObjects.Cadastro;
using CadastroUsuario.Repository.Interfaces.Cadastro;
using CadastroUsuario.Service.Interfaces.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroUsuario.Service.Cadastro
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioConverter _usuarioConverter;

        public UsuarioService(IUsuarioRepository usuarioRepository, UsuarioConverter usuarioConverter)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioConverter = usuarioConverter;
        }

        /// <summary>
        /// Cadastra um novo usuario
        /// </summary>
        /// <param name="usuaroVo"></param>
        /// <returns></returns>
        public UsuarioVo Post(UsuarioVo usuarioVo)
        {
            var response = _usuarioRepository.Add(_usuarioConverter.Parse(usuarioVo));

            return _usuarioConverter.Parse(response);
        }

        /// <summary>
        /// Atualiza o cadastro do usuario
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <returns></returns>
        public UsuarioVo Put(UsuarioVo usuarioVo)
        {
            var usuarioEntity = _usuarioConverter.Parse(usuarioVo);

            if (usuarioVo != null)
                _usuarioRepository.Update(usuarioEntity);

            return _usuarioConverter.Parse(usuarioEntity);
        }

        /// <summary>
        /// Remove um registro de usuario
        /// </summary>
        /// <param name="guidUsuario"></param>
        /// <returns></returns>
        public bool Remove(Guid? guidUsuario)
        {
            var usuario = _usuarioRepository.GetByGuid(guidUsuario);

            if (usuario == null)
                return false;

            _usuarioRepository.Remove(usuario);
            return true;
        }

        /// <summary>
        /// Lista todos usuarios ativos cadastrados
        /// </summary>
        /// <returns></returns>
        public List<UsuarioVo> GetUsuariosAtivos() =>
            _usuarioConverter.ParseList(_usuarioRepository.GetAll().Where(x => x.Status.Equals('A')).ToList());
    }
}
