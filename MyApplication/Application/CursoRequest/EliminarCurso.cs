using Application.ManejadorErr;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CursoRequest
{
    public class EliminarCurso
    {
        public class Ejecutar : IRequest
        {
            public int Cursoid { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly ContextoCurso Context;
            public Manejador(ContextoCurso context)
            {
                this.Context = context;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var cursos = await Context.TCurso.FindAsync(request.Cursoid);
                if (cursos == null)
                {
                    throw new ErrorHandler(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el curso" });
                }
                Context.Remove(cursos);
                var result = await Context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puede eliminar el curso");
            }
        }
    }
}
