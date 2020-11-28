using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;
namespace WebApiRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ContextoCurso Context;
        public WeatherForecastController(ContextoCurso context)
        {
            this.Context = context;
        }
        [HttpGet]
        public IEnumerable<Curso> GetAll()
        {
            return (Context.TCurso.ToList());
        }
    }
}
