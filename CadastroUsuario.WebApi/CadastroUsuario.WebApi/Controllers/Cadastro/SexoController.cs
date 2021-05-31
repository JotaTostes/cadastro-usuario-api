using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroUsuario.Service.Interfaces.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace CadastroUsuario.WebApi.Controllers.Cadastro
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SexoController : ControllerBase
    {
        private readonly ISexoService _sexoService;

        public SexoController(ISexoService sexoService)
        {
            _sexoService = sexoService;
        }

        /// <summary>
        /// Retorna todos registros de sexo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll() =>
            Ok(_sexoService.GetAll());
    }
}
