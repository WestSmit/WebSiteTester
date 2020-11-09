using System.Data.Entity;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestedSite>().HasMany(c => c.Pages);

            modelBuilder.Entity<TestedSite>().HasIndex(s => s.Url).IsUnique(true);

            modelBuilder.Entity<TestedSite>().Property(p => p.Url).HasMaxLength(150);

            modelBuilder.Entity<TestedPage>().HasMany(c => c.Results);

            modelBuilder.Entity<TestedPage>().HasIndex(c => c.Url).IsUnique(true);

            modelBuilder.Entity<TestedPage>().Property(p => p.Url).HasMaxLength(150);
        }
    }
}
