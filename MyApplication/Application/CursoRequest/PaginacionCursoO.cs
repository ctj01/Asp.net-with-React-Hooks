using MediatR;
using Persistencia.Paginacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CursoRequest
{
    public class PaginacionCursoO
    {
        public class ejecuta : IRequest<PaginacionModel>
        {
            public string Titulo { get; set; }
            public int NumeroDePaginas { get; set; }
            public int CantidadDeElementos { get; set; }
        }
        public class Manejador : IRequestHandler<ejecuta, PaginacionModel>
        {
            private readonly IPaginacion paginacion;
            public Manejador(IPaginacion paginacion)
            {
                this.paginacion = paginacion;
            }
            public async Task<PaginacionModel> Handle(ejecuta request, CancellationToken cancellationToken)
            {
                var storeprocedure = "Paginacion_Curso";
                var ordenamiento = "Titulo";
                var parametros = new Dictionary<string, object>();
                parametros.Add("@NombreCurso", request.Titulo);

                return await paginacion.DevolverPaginacion(
                    storeprocedure,
                    request.NumeroDePaginas,
                    request.CantidadDeElementos,
                    parametros,
                    ordenamiento
                    );
            }
        }
    }
}
