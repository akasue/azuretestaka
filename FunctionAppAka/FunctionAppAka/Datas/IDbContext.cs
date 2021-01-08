using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FunctionAppAka.Datas
{
   
    public interface IDbContext
    {
        
        IDbConnection Connection { get; }

      
        int Execute(string sql, object param = null, IDbTransaction transaction = null);

     
        IEnumerable<T> Query<T>(string sql, object param = null);
    }
}
