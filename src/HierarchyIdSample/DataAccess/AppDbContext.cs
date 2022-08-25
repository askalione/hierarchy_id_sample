using HierarchyIdSample.Entities;

namespace HierarchyIdSample.DataAccess
{
    internal class AppDbContext : DbContext, IDbContext
    {
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Counter> Counters { get; set; } = default!;

        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
