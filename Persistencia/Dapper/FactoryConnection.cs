using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.Dapper
{
    public class FactoryConnection : IFactoryConection
    {
        private IDbConnection conection;
        private readonly IOptions<DbConectionConfig> conf;
        public FactoryConnection(IOptions<DbConectionConfig> conexion)
        {
            this.conf = conexion;
        }
        public void closeConection()
        {
            if (conection.State == ConnectionState.Open)
            {
                conection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (conection == null)
            {
                conection = new SqlConnection(conf.Value.CursoConection);
            }
            if (conection.State != ConnectionState.Open)
            {
                conection.Open();
            }
            return conection;
        }
    }
}
