using Application.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.Controllers
{
    public class RolController : BaseController
    {
        [HttpPost("Crear")]
        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta data)
        {
            return await Mediador.Send(data);
        }
        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(EliminarRol.Ejecuta data)
        {
            return await Mediador.Send(data);
        }
        [HttpGet]

        public async Task<ActionResult<List<IdentityRole>>> Lista()
        {
            return await Mediador.Send(new ObtenerListaRoles.Ejecuta());        
        }
        [HttpPost("AsignarRol")]

        public async Task<ActionResult<Unit>> AsignarRol(AgregarRolUsuario.Ejecuta data)
        {
            return await Mediador.Send(data);
        }
        [HttpDelete("EliminarRol")]

        public async Task<ActionResult<Unit>> EliminarUsuario(UsuarioRolEliminar.Ejecutar data)
        {
            return await Mediador.Send(data);
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<List<string>>> GetAllRolesUser(string UserName)
        {
            
            return await Mediador.Send(new ListarUsuarioRolesPorUsername.Ejecuta { Username = UserName});
        }

    }
}
