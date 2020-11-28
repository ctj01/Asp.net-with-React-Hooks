using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComentariosRequest
{
    public  class NuevoComentario
    {
        public class Ejecutar : IRequest
        {
            public string Alumno { get; set; }
            public int Puntaje { get; set; }
            public string ComentarioTexto { get; set; }
            public Guid Cursoid { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly ContextoCurso contexto;
            public Manejador(ContextoCurso contexto)
            {
                this.contexto = contexto;
            }
            public class Validad : AbstractValidator<Ejecutar>
            {
                public Validad()
                {
                    RuleFor(x => x.Alumno).NotEmpty();
                    RuleFor(x => x.Puntaje).NotEmpty();
                    RuleFor(x => x.ComentarioTexto).NotEmpty();
                    RuleFor(x => x.Cursoid).NotEmpty();
                }
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var Nuevocomentario = new Comentario
                {
                    Comentarioid = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    Puntaje = request.Puntaje,
                    ComentarioTexto = request.ComentarioTexto,
                    Cursoid = request.Cursoid,
                    FechaCreacion = DateTime.UtcNow
                    
                    
                };
                await contexto.TComentario.AddAsync(Nuevocomentario);
                var result = await contexto.SaveChangesAsync();
                if (result > 0)
                {
                    return (Unit.Value);
                }
                throw new ManejadorErr.ErrorHandler(System.Net.HttpStatusCode.BadRequest, new {mensaje = " no se pudo insertar el comentario" });
            }
        }
    }
}
