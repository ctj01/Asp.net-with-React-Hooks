using Application.DTO;
using Application.ManejadorErr;
using AutoMapper;
using Dominio.Entidades;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CursoRequest
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            public Guid CursoId { get; set; }
            public CursoUnico(Guid Cursoid)
            {
                this.CursoId = Cursoid;
                
            }
        }
        public class Manejador : IRequestHandler<CursoUnico, CursoDto>
        {
            private readonly ContextoCurso ContextCurso;
            public IMapper Mapper { get; set; }

            public Manejador(ContextoCurso contextoCurso, IMapper mapper)
            {
                this.ContextCurso = contextoCurso;
                this.Mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var cursosId = await ContextCurso.TCurso
                    .Include(x => x.Comentarios)
                    .Include(x => x.Tprecio)
                    .Include(x => x.TinstructorCursos)
                    .ThenInclude(x => x.Tinstructor).FirstOrDefaultAsync(a => a.Cursoid == request.CursoId);
                if (cursosId == null)
                {
                    throw new ErrorHandler(HttpStatusCode.NotFound, new { Mensaje = "No se encontro el curso" });
                }
                var cursoidDto = Mapper.Map<Curso, CursoDto>((Curso)cursosId);
                return cursoidDto;
            }
        }
    }
}
