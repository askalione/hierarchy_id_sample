using System.Data;

namespace HierarchyIdSample.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection();
    }
}
