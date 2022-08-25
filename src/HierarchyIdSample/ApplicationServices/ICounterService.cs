namespace HierarchyIdSample.ApplicationServices
{
    public interface ICounterService
    {
        Task<int> GetValueAsync(string name, CancellationToken cancellationToken = default);
    }
}
