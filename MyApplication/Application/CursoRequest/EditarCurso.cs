using Application.ManejadorErr;
using FluentValidation;
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
    public class EditarCurso
    {
        public class Ejecutar : IRequest 
        {
            public int Cursoid { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime ? Fechadepublicacion { get; set;}
            public Ejecutar(int cursoid, string titulo, string descripcion, DateTime fechapublicacion)
            {
                this.Cursoid = cursoid;
                this.Titulo = titulo;
                this.Descripcion = descripcion;
                this.Fechadepublicacion = fechapublicacion;
            }
            public Ejecutar()
            {

            }
        }
        public class Validacion : AbstractValidator<Ejecutar>
        {
            public Validacion()
            {
                RuleFor(x => x.Titulo).NotEmpty().NotNull().WithMessage("Los campos estan vacios");
                RuleFor(x => x.Descripcion).NotEmpty().NotNull().WithMessage("Los campos estan vacios");
                RuleFor(x => x.Fechadepublicacion).NotEmpty().NotNull().WithMessage("Los campos estan vacios");
            }
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
                var Cursos = await Context.TCurso.FindAsync(request.Cursoid);
                if (Cursos == null)
                {
                    throw new ErrorHandler(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el curso" });
                }
                Cursos.Titulo = request.Titulo ?? Cursos.Titulo;
                Cursos.Descripcion = request.Descripcion ?? Cursos.Descripcion;
                Cursos.Fechadepublicacion = request.Fechadepublicacion ?? Cursos.Fechadepublicacion;
                var Result = await Context.SaveChangesAsync();
                if (Result < 0 )
                {
                    throw new Exception("No se pudo modificar el curso");
                }

                return Unit.Value;
            }
        }
    }
}
