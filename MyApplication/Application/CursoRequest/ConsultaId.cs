using Application.ManejadorErr;
using Dominio.Entidades;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CursoRequest
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>
        {
            public int Id { get; set; }
            public CursoUnico(int id)
            {
                this.Id = id;
            }
        }
        public class Manejador : IRequestHandler<CursoUnico, Curso>
        {
            private readonly ContextoCurso ContextCurso;
           
            public Manejador(ContextoCurso contextoCurso)
            {
                this.ContextCurso = contextoCurso;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var cursosId = await ContextCurso.FindAsync(typeof(Curso), request.Id);
                if (cursosId == null)
                {
                    throw new ErrorHandler(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el curso" });
                }
                return (Curso)(cursosId);
            }
        }
    }
}
