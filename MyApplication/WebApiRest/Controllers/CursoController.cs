using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CursoRequest;
using Application.DTO;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<CursoDto>>> GetAll()
        {
            return await Mediador.Send(new Consulta.ListaCurso());
        }
        [HttpGet("{Cursoid}")]
        public async Task<ActionResult<CursoDto>> GetForId(Guid CursoId)
        {
            return await Mediador.Send(new ConsultaId.CursoUnico(Cursoid:CursoId)) ;
        }
        [HttpPost("{titulo}/{descripcion}/{fechadepublicacion}")]
        public async Task<ActionResult<Unit>> Insertar(string titulo, string descripcion, DateTime fechaDePublicacion, decimal PrecioActual, decimal Promocion)
        {   
            return await Mediador.Send(new NuevoCurso.Ejecutar(Titulo: titulo, Descripcion:descripcion, FechaDePublicacion:fechaDePublicacion, Precioactual:PrecioActual, Promocion: Promocion));
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(NuevoCurso.Ejecutar data)
        {
            return await Mediador.Send(data);
        }
        [HttpPut]
        public async Task<ActionResult<Unit>> Modificar(Guid Cursoid, string titulo, string descripcion, DateTime fechaDePublicacion, decimal PrecioActual, decimal Promocion)
        {
            return await Mediador.Send(new EditarCurso.Ejecutar(cursoid: Cursoid, titulo: titulo, descripcion: descripcion, fechapublicacion: fechaDePublicacion, PrecioActual: PrecioActual, Promocion: Promocion));
        }
        [HttpPut("{Cursoid}")]
        public async Task<ActionResult<Unit>> Modificar(Guid Cursoid, EditarCurso.Ejecutar data)
        {
            data.Cursoid = Cursoid;
            return await Mediador.Send(data);
        }
        [HttpDelete("{Cursoid}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid Cursoid)
        {
            return await Mediador.Send(new EliminarCurso.Ejecutar { Cursoid = Cursoid });
        }

    }
}
