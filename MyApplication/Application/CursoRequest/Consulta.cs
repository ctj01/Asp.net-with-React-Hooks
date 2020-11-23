using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using Application.DTO;
using AutoMapper;

namespace Application.CursoRequest
{
    public class Consulta
    {
        public class ListaCurso : IRequest<List<CursoDto>> { }
        public class Manejador : IRequestHandler<ListaCurso, List<CursoDto>>
        {
            private readonly ContextoCurso Context;
            private IMapper imapper;
            public Manejador(ContextoCurso Context, IMapper mapper)
            {
                this.Context = Context;
                this.imapper = mapper;
            }
            public async Task<List<CursoDto>> Handle(ListaCurso request, CancellationToken cancellationToken)
            {
                var CursosLista = await Context.TCurso
                    .Include(x => x.Comentarios)
                    .Include(x => x.Tprecio)
                    .Include(x => x.TinstructorCursos)
                    .ThenInclude(x => x.Tinstructor).ToListAsync();
                var cursodto = imapper.Map<List<Curso>, List<CursoDto>>(CursosLista);

                return cursodto;
            }
        }
    }
}
