using FluentValidation;
using MediatR;
using Persistencia.InstructorModelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorRequest
{
    public class NuevoInstructor
    {
        public class Ejecutar : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Grado { get; set; }
        }
        public class Validar: AbstractValidator<Ejecutar> 
        {
            public Validar()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.Grado).NotEmpty();
            }        
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly IInstructor<InstructoModel> instructor;
            public Manejador(IInstructor<InstructoModel> instructor)
            {
                this.instructor = instructor;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var nuevo = new InstructoModel
                {   Instructorid = Guid.NewGuid(),
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Grado = request.Grado
                };
                var resultado = await instructor.NuevoInstructor(nuevo);
                if (resultado > 0)
                {
                    return (Unit.Value);
                }
                throw new Exception("no se pudo registrar el instructor");
            }
        }
    }
}
