using Dapper;
using Persistencia.Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Persistencia.Paginacion
{
    public class PaginacionRespository : IPaginacion
    {
        private readonly IFactoryConection conection;
        public PaginacionRespository(IFactoryConection conection)
        {
            this.conection = conection;
        }
        public async Task<PaginacionModel> DevolverPaginacion(string Procedure, int Numerodepaginas, int cantidad, IDictionary<string, object> Parametros, string Ordenamiento)
        {
            PaginacionModel paginacion = new PaginacionModel();
            List<IDictionary<string, object>> ListaReporte;
            DynamicParameters parameters = new DynamicParameters();
            int total_records = 0;
            int total_paginas = 0;
            foreach (var param in Parametros)
            {
                parameters.Add("@" + param.Key, param.Value);
            }
            parameters.Add("@NumeroDePaginas", Numerodepaginas);
            parameters.Add("@CantidadDeElementos", cantidad);
            parameters.Add("@Ordenamiento", Ordenamiento);
            parameters.Add("@Total_records", total_records, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            parameters.Add("@Total_paginas", total_paginas, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            paginacion.NumeroDepaginas = parameters.Get<int>("Total_paginas");
            paginacion.TotalRecord = parameters.Get<int>("Total_records");

            try
            {
                var conexion = conection.GetConnection();
                var result =  await conexion.QueryAsync(Procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                ListaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacion.ListaRecord = ListaReporte;
            }
            catch (Exception ex)
            {

                throw new Exception("Error {0}", ex);
            }
            return paginacion;
        }
    }
}
