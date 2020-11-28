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
    public class ListarUsuarioRolesPorUsername
    {
        public class Ejecuta:IRequest<List<string>>
        {
            public string Username { get; set; }
        }
        public class Manejador: IRequestHandler<Ejecuta, List<string>>
        {
            private readonly UserManager<Usuario> user;
            private readonly RoleManager<IdentityRole> manager;
            public Manejador(UserManager<Usuario> user, RoleManager<IdentityRole> manager)
            {
                this.user = user;
                this.manager = manager;
            }

            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result =  await user.FindByNameAsync(request.Username);

                if (result == null)
                {
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new { mensaje =  "El user name no existe"});
                }
                var role = await user.GetRolesAsync(result);

                return new List<string>(role);
            }
        }
    }
}
