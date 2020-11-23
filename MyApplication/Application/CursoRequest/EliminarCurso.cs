using Application.ManejadorErr;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public Guid Cursoid { get; set; }
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
                var CursosDB = Context.InstructorCurso.Where(x => x.Cursoid == request.Cursoid);

                foreach (var curso in CursosDB)
                {
                    Context.InstructorCurso.Remove(curso);
                }
                var comentarios = Context.TComentario.Where(x => x.Cursoid == request.Cursoid);
                if (comentarios != null)
                {
                    foreach (var id in comentarios)
                    {
                        Context.TComentario.Remove(id);
                    }
                }
                var precio = Context.TPrecio.Where(x => x.Cursoid == request.Cursoid).FirstOrDefault();
                if (precio != null)
                {
                    Context.TPrecio.Remove(precio);
                }
                
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
