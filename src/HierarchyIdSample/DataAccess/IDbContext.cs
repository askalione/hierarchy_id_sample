using HierarchyIdSample.Entities;

namespace HierarchyIdSample.DataAccess
{
    public interface IDbContext
    {
        DbSet<Department> Departments { get; }
        DbSet<Counter> Counters { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
