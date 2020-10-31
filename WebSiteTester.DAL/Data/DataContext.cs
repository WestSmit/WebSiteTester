using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSiteTester.DAL.Entities;

namespace WebSiteTester.DAL.Data
{
    public class DataContext : DbContext
    {
        public DbSet<TestedSite> TestedSites { get; set; }
        public DbSet<TestedPage> TestedPages { get; set; }

        public DataContext(string connectionString) : base(connectionString)
        {
        }
    }
}
