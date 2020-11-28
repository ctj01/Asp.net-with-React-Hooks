using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Paginacion
{
    public interface IPaginacion
    {
        Task<PaginacionModel> DevolverPaginacion(string Procedure,
        int Numerodepaginas,
        int cantidad,
        IDictionary<string, object> Parametros,
        string Ordenamiento);

    }
}
