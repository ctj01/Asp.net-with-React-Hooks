using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class RolNuevo
    {
        public class Ejecuta:IRequest
        {
            public string Nombre { get; set; }

        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> Rol;
            public Manejador(RoleManager<IdentityRole> Rol)
            {
                this.Rol = Rol;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var Role = await Rol.FindByNameAsync(request.Nombre);

                if (Role != null)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new { Mensaje = "Ya existe el rol" });
                }
                var result =  await Rol.CreateAsync(new IdentityRole(request.Nombre));
                if (result.Succeeded)
                    return Unit.Value;
                throw new Exception("No se pudo crear el rol");
            }
        }

    }
}
