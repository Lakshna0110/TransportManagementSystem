using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace util
    {
        public static class DBPropertyUtil
        {
        public static string GetConnectionString()
        {
            
            return @"Data Source=localhost\\SQLEXPRESS,1433;Initial Catalog=TransportManagement;Integrated Security=True";
        }
    }
}
    

