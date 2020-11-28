using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Paginacion
{
    public class PaginacionModel
    {
        public List<IDictionary<string, object>> ListaRecord { get; set; }
        public int TotalRecord { get; set; }
        public int NumeroDepaginas { get; set; }

    }
}
