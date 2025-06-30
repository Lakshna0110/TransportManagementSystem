using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using model;



namespace util
{
    public class TmsDbContext : DbContext
    {
        public TmsDbContext() : base(DBPropertyUtil.GetConnectionString())
        {
        }

        public DbSet<Payment> Payments { get; set; }
    }
}
    



