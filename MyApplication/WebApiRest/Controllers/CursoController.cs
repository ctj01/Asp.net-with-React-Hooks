using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CursoRequest;
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
        public async Task<ActionResult<List<Curso>>> GetAll()
        {
            return await Mediador.Send(new Consulta.ListaCurso());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetForId(int Id)
        {
            return await Mediador.Send(new ConsultaId.CursoUnico(id:Id)) ;
        }
        [HttpPost("{titulo}/{descripcion}/{fechadepublicacion}")]
        public async Task<ActionResult<Unit>> Insertar(string titulo, string descripcion, DateTime fechaDePublicacion)
        {   
            return await Mediador.Send(new NuevoCurso.Ejecutar(Titulo: titulo, Descripcion:descripcion, FechaDePublicacion:fechaDePublicacion));
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(NuevoCurso.Ejecutar data)
        {
            return await Mediador.Send(data);
        }
        [HttpPut]
        public async Task<ActionResult<Unit>> Modificar(int id, string titulo, string descripcion, DateTime fechaDePublicacion)
        {
            return await Mediador.Send(new EditarCurso.Ejecutar(cursoid: id, titulo: titulo, descripcion: descripcion, fechapublicacion: fechaDePublicacion));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Modificar(int id, EditarCurso.Ejecutar data)
        {
            data.Cursoid = id;
            return await Mediador.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await Mediador.Send(new EliminarCurso.Ejecutar { Cursoid = id });
        }

    }
}
