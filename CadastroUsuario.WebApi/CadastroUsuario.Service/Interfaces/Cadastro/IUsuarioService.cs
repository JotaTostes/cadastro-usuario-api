﻿using CadastroUsuario.DataVo.ValueObjects.Cadastro;
using CadastroUsuario.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Service.Interfaces.Cadastro
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        /// <summary>
        /// Cadastra um novo usuario
        /// </summary>
        /// <param name="usuaroVo"></param>
        /// <returns></returns>
        UsuarioVo Post(UsuarioVo usuarioVo);

        /// <summary>
        /// Atualiza o cadastro do usuario
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <returns></returns>
        UsuarioVo Put(UsuarioVo usuarioVo);

        /// <summary>
        /// Remove um registro de usuario
        /// </summary>
        /// <param name="guidUsuario"></param>
        /// <returns></returns>
        bool Remove(int id);

        /// <summary>
        /// Lista todos usuarios ativos cadastrados
        /// </summary>
        /// <returns></returns>
        List<UsuarioVo> GetUsuariosAtivos(); 
    }
}
