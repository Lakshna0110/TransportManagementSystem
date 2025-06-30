using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
    {
        public static class DBConnUtil
        {
            public static SqlConnection GetConnection()
            {
                string connStr = DBPropertyUtil.GetConnectionString();
                return new SqlConnection(connStr);
            }
        }
    }
