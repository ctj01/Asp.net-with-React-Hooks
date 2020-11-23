using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CursoRequest
{
   public class NuevoCurso
    {
        public class Ejecutar : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime Fechadepublicacion { get; set; }
            public decimal PrecioActual { get; set; }
            public decimal Promocion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public Ejecutar(string Titulo, string Descripcion, DateTime FechaDePublicacion, decimal Precioactual, decimal Promocion)
            {
                this.Titulo = Titulo;
                this.Descripcion = Descripcion;
                this.Fechadepublicacion = FechaDePublicacion;
                this.Promocion = Promocion;
                this.PrecioActual = Precioactual;
            }
            public Ejecutar() { }
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
            public Manejador(ContextoCurso Context)
            {
                this.Context = Context;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Guid _Cursoid = Guid.NewGuid();
                var cursos = new Curso
                {
                    Cursoid = _Cursoid,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    Fechadepublicacion = request.Fechadepublicacion
                };
                Context.TCurso.Add(cursos);
                if (request.ListaInstructor != null)
                {
                    foreach (var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new InstructorCurso
                        {
                            Cursoid = _Cursoid,
                            Instructorid = id
                        };
                        Context.InstructorCurso.Add(cursoInstructor);
                    }
                }
                var precioentidad = new Precio
                {
                    Cursoid = _Cursoid,
                    PrecioActual = request.PrecioActual,
                    Promocion = request.Promocion,
                    Precioid = Guid.NewGuid()

                };
                Context.TPrecio.Add(precioentidad);
                var valor = await Context.SaveChangesAsync();
                if (valor > 0)
                {
                    return (Unit.Value);
                }
                throw new Exception("No se pudo ingresar el curso");
            }
        }
    }
}
