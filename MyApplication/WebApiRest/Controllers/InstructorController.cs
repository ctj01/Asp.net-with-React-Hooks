using Application.InstructorRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.InstructorModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace WebApiRest.Controllers
{
    public class InstructorController:BaseController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<InstructoModel>>> ListaInstructores()
        {
            var list =await Mediador.Send(new ConsultaInstructor.Consulta());
            return list.ToList();
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> InstructorNuevo(NuevoInstructor.Ejecutar Data)
        {
            return await Mediador.Send(Data);
        }
        [HttpPut("{InstructorId}")]
        public async Task<ActionResult<Unit>> EditarInstructor(Guid InstructorId ,EditarIntructor.Ejecuta data)
        {
            data.Instructorid = InstructorId;
            return await Mediador.Send(data);
        }
        [HttpDelete("{InstructorId}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid InstructorId)
        {
            return await (Mediador.Send(new EliminarInstructor.Ejecuta { InstructorId = InstructorId }));
        }
    }
}
