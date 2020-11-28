using Application.ManejadorErr;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiRest.MiddleWare
{
    public class ManejadorErrorMiddleWare
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<ManejadorErrorMiddleWare> Logger;
        public ManejadorErrorMiddleWare(RequestDelegate next, ILogger<ManejadorErrorMiddleWare> Logger)
        {
            this.Next = next;
            this.Logger = Logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);

            }
            catch (Exception ex)
            {

                await ManejadorDeExcepciones(httpContext, ex, Logger);
            } 
        }
        private async Task ManejadorDeExcepciones(HttpContext httpContext , Exception ex, ILogger<ManejadorErrorMiddleWare> logger)
        {
            object Errores = null;
            switch (ex)
            {
                    case ErrorHandler Err:
                    logger.LogError(ex, "Manejador de excepciones");
                    Errores = Err.Error;
                    httpContext.Response.StatusCode = (int)Err.Codigo;
                    break;
                    case Exception e:
                    logger.LogError(ex, "Error en el servidor");
                    Errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            httpContext.Response.ContentType = "application/json";

            if (Errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { Errores });
                await httpContext.Response.WriteAsync(resultados);
            }
        }
    }
}

