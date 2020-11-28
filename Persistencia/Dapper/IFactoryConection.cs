using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.Dapper
{
    public interface IFactoryConection
    {
        void closeConection();
        IDbConnection GetConnection();
    }
}
