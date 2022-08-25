using HierarchyIdSample.UseCases.Commands.Create;
using HierarchyIdSample.UseCases.Commands.Edit;

namespace HierarchyIdSample
{
    public class App
    {
        private readonly IOperationExecutor _executor;

        public App(IOperationExecutor executor)
        {
            Ensure.NotNull(executor, nameof(executor));

            _executor = executor;
        }

        public async Task RunAsync()
        {
            await CreateDepartmentAsync(null, "Root");
            await CreateDepartmentAsync(1, "Dep-1");
            await CreateDepartmentAsync(1, "Dep-2");
            await CreateDepartmentAsync(2, "Dep-1-2");
            await CreateDepartmentAsync(2, "Dep-1-3");

            await EditDepartmentAsync(5, 3, "Dep-1-3-success");
            await EditDepartmentAsync(2, 4, "Dep-1-failure");
        }

        private Task CreateDepartmentAsync(int? parentId, string name)
            => _executor.ExecuteAsync(new DepartmentCreateCommand
            {
                ParentId = parentId,
                Name = name
            });

        private Task EditDepartmentAsync(int id, int? parentId, string name)
            => _executor.ExecuteAsync(new DepartmentEditCommand
            {
                Id = id,
                ParentId = parentId,
                Name = name
            });
    }
}
