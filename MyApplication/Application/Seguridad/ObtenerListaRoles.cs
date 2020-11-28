using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class ObtenerListaRoles
    {
        public class Ejecuta : IRequest<List<IdentityRole>>
        { }

        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            private readonly ContextoCurso contexto;
            public Manejador(ContextoCurso contexto)
            {
                this.contexto = contexto;
            }
            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result =  await contexto.Roles.ToListAsync();

                return result;
            }
        }
    }
}
