using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FunctionAppAka.Datas
{
   
    public class ConnectionManager
    {
        private readonly string _connStr;
        
        public ConnectionManager(string connStr)
        {
            _connStr = connStr;
        }


        public IDbConnection Connection => new SqlConnection(_connStr);
    }
}
