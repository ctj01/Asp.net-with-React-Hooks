using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class AgregarRolUsuario
    {
        public class Ejecuta : IRequest
        {
            public string UserName { get; set; }
            public string Rol { get; set; }
        }
        public class Validar: AbstractValidator<Ejecuta>
        {
            public Validar()
            {
                RuleFor(x => x.Rol).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
                this.userManager = userManager;
            }
            public  async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new { Mensaje = "No existe el usuario" });
                }
                var rol = await roleManager.FindByNameAsync(request.Rol);

                if (rol == null)
                    throw new Exception("El rol No existe");

                var result = await userManager.AddToRoleAsync(user, request.Rol);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }
              
                throw new Exception("No Se Pudo asignar el rol");
            }
        }
    }
}
