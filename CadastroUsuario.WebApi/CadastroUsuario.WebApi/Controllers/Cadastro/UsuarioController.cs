using CadastroUsuario.DataVo.ValueObjects.Cadastro;
using CadastroUsuario.Service.Interfaces.Cadastro;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuario.WebApi.Controllers.Cadastro
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Adiciona um novo usuario
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromBody] UsuarioVo usuarioVo) =>
            Ok(_usuarioService.Post(usuarioVo));

        /// <summary>
        /// Atualiza o cadastro do usuario
        /// </summary>
        /// <param name="usuarioVo"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] UsuarioVo usuarioVo) =>
            Ok(_usuarioService.Put(usuarioVo));

        /// <summary>
        /// Remove um registro de usuario
        /// </summary>
        /// <param name="guidUsuario"></param>
        /// <returns></returns>
        [HttpDelete("Guid/{guidUsuario}")]
        public ActionResult Remove(Guid? guidUsuario) =>
            Ok(_usuarioService.Remove(guidUsuario));

        /// <summary>
        /// Lista todos usuarios ativos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUsuariosAtivos() =>
            Ok(_usuarioService.GetUsuariosAtivos());
    }
}
