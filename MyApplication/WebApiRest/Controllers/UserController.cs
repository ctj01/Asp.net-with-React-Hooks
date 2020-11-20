using Application.Seguridad;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.Controllers
{
    [AllowAnonymous]
    public class UserController:BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> login(Login.Ejecutar parametros)
        {
            return await Mediador.Send(parametros);
        }
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecutar Parametros)
        {
            return (await Mediador.Send(Parametros));
        }
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> ObtenerUsuario()
        {
            return await Mediador.Send(new UsuarioActual.Ejecutar());
        }
    }
}
