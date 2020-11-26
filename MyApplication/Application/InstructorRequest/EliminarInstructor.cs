using Application.ManejadorErr;
using MediatR;
using Persistencia.InstructorModelos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorRequest
{
    public class EliminarInstructor
    {
        public class Ejecuta : IRequest
        {
            public Guid InstructorId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor<InstructoModel> instructor;
            public Manejador( IInstructor<InstructoModel> instructor)
            {
                this.instructor = instructor;
            }
            public  async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultado = await instructor.Eliminar(request.InstructorId);
                if (resultado > 0)
                {
                    return (Unit.Value);
                }
                throw new ErrorHandler(HttpStatusCode.BadRequest, new {Mensaje = "No se Pudo Elimar El Registro"});
            }
        }
    }
}
