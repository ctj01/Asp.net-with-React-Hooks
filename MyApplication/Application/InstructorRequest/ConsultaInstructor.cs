using MediatR;
using Persistencia.InstructorModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorRequest
{
    public class ConsultaInstructor
    {
        public class Consulta: IRequest<List<InstructoModel>> { }
        public class ProcedureExecute : IRequestHandler<Consulta, List<InstructoModel>>
        {
            IInstructor<InstructoModel> _instructor;
            public ProcedureExecute(IInstructor<InstructoModel> instructor)
            {
                this._instructor = instructor;
            }
            public async Task<List<InstructoModel>> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var resultado = await _instructor.Getall();
                return resultado.ToList();
             
            }
        }
    }
}
