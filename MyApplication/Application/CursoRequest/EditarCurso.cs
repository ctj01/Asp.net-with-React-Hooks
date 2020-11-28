using Application.ManejadorErr;
using AutoMapper;
using Dominio.Entidades;
using FluentValidation;
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
    public class EditarCurso
    {
        public class Ejecutar : IRequest 
        {
            public Guid Cursoid { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime ? Fechadepublicacion { get; set;}
            public decimal ? PrecioActual { get; set; }
            public decimal ? Promocion { get; set; }
            public List<Guid> InstructorLista { get; set; }
            public Ejecutar(Guid cursoid, string titulo, string descripcion, DateTime fechapublicacion, decimal PrecioActual, decimal Promocion)
            {
                this.Cursoid = cursoid;
                this.Titulo = titulo;
                this.Descripcion = descripcion;
                this.Fechadepublicacion = fechapublicacion;
                this.PrecioActual = PrecioActual;
                this.Promocion = Promocion;
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
            private IMapper mapper;
            public Manejador(ContextoCurso context, IMapper mapper)
            {
                this.Context = context;
                this.mapper = mapper;
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
                Cursos.FechaCreacion = DateTime.UtcNow;

                var Precioentidad = Context.TPrecio.Where(x => x.Cursoid == request.Cursoid).FirstOrDefault();

                if (Precioentidad != null)
                {
                    Precioentidad.PrecioActual = request.PrecioActual ?? Precioentidad.PrecioActual;
                    Precioentidad.Promocion = request.Promocion ?? Precioentidad.Promocion;
                }
                else 
                {
                    var nuevoprecio = new Precio
                    {
                        Precioid = Guid.NewGuid(),
                        PrecioActual = request.PrecioActual ?? 0,
                        Promocion = request.Promocion ?? 0,
                        Cursoid = request.Cursoid
                        
                        
                    };
                    await Context.TPrecio.AddAsync(nuevoprecio);
                }


                if (request.InstructorLista != null)
                {
                    var InstructoresDb = Context.InstructorCurso.Where(x => x.Cursoid == request.Cursoid);
                    foreach (var Idb in InstructoresDb)
                    {
                        Context.InstructorCurso.Remove(Idb);
                    }
                    foreach (var id in request.InstructorLista)
                    {
                        var nuevo = new InstructorCurso
                        {
                            Cursoid = request.Cursoid,
                            Instructorid = id

                        };
                        Context.InstructorCurso.Add(nuevo);
                    }
                }
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
