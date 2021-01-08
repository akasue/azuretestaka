using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FunctionAppAka.Datas
{
   
    public class DbContext : IDbContext
    {
        private ConnectionManager _manager;

       
        public DbContext(ConnectionManager manager)
        {
            _manager = manager;
        }

      
        public IDbConnection Connection
        {
            get
            {
                return _manager.Connection;
            }
        }

      
        public int Execute(string sql, object param = null, IDbTransaction transaction = null)
        {
            return _manager.Connection.Execute(sql, param, transaction);
        }

       
        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return _manager.Connection.Query<T>(sql, param);
        }
    }
}
