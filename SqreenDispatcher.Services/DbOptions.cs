using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services
{
    public class DbOptions
    {
        public DbOptions(string connString)
        {
            ConnectionString = connString;
        }
        public string ConnectionString { get; set; }
    }
}
