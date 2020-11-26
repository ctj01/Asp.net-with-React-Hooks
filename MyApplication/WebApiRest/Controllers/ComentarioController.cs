using Application.ComentariosRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRest.Controllers
{
    public class ComentarioController:BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar( NuevoComentario.Ejecutar Data)
        {
            return await Mediador.Send(Data);
        }
        [HttpDelete("{ComentarioId}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid ComentarioId)
        {
            
            return await Mediador.Send(new EliminarComentario.Ejecutar { ComentarioId = ComentarioId});
        }
    }
}
