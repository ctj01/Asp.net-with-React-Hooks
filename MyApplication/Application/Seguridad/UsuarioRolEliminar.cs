using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class UsuarioRolEliminar
    {
        public class Ejecutar : IRequest
        {
            public string UserName { get; set; }
            public string Rol { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var roles = await roleManager.FindByNameAsync(request.Rol);
                if (roles == null)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new { Mensaje = "No exite el rol" });
                }
                var username = await userManager.FindByNameAsync(request.UserName);

                if (username == null)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new { Mensaje = "No existe el usuario" });
                }
                var result = await userManager.RemoveFromRoleAsync(username, request.Rol);
                if (result.Succeeded)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo elimar el rol");
            }
        }
    }
}
