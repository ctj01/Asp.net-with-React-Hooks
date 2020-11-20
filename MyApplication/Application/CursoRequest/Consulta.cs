using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Application.CursoRequest
{
    public class Consulta
    {
        public class ListaCurso : IRequest<List<Curso>> { }
        public class Manejador : IRequestHandler<ListaCurso, List<Curso>>
        {
            private readonly ContextoCurso Context;
            public Manejador(ContextoCurso Context)
            {
                this.Context = Context;
            }
            public async Task<List<Curso>> Handle(ListaCurso request, CancellationToken cancellationToken)
            {
                var CursosLista = await Context.TCurso.ToListAsync();
                return CursosLista;
            }
        }
    }
}
