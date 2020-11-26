using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComentariosRequest
{
    public class EliminarComentario
    {
        public class Ejecutar: IRequest
        {
            public Guid ComentarioId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly ContextoCurso contexto;
            public Manejador(ContextoCurso contexto)
            {
                this.contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var Curso = await contexto.TComentario.FindAsync(request.ComentarioId);
                
                if (Curso == null)
                    throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.NotFound, new { Mensaje = "Comentaio no encontado" });
                
                contexto.TComentario.Remove(Curso);
               
                var resultado =  await contexto.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el comentario");
            }
        }
    }
}
