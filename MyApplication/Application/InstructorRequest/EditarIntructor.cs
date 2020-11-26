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
    public class EditarIntructor
    {
        public class Ejecuta: IRequest
        {
            public Guid Instructorid { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Grado { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            IInstructor<InstructoModel> Instructor;
            public Manejador(IInstructor<InstructoModel> instructo)
            {
                this.Instructor = instructo;
            }
            public class Validar:AbstractValidator<Ejecuta>
            {
                public Validar()
                {
                    //RuleFor(x => x.Instructorid).NotEmpty();
                    RuleFor(x => x.Nombre).NotEmpty();
                    RuleFor(x => x.Apellido).NotEmpty();
                    RuleFor(x => x.Grado).NotEmpty();
                }

            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var nuevo = new InstructoModel { 
                Instructorid = request.Instructorid,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Grado = request.Grado
                };
     

                var result = await Instructor.Actualizar(nuevo);
                if (result > 0)
                {
                    return (Unit.Value);
                }
                throw new Exception("no se puede actualizar el registro");
            }
        }
    }
}
