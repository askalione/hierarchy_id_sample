using HierarchyIdSample.ApplicationServices;
using HierarchyIdSample.Entities;

namespace HierarchyIdSample.UseCases.Commands.Create
{
    internal class DepartmentCreateCommandHandler : AsyncOperationHandler<DepartmentCreateCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly ICounterService _counterService;

        public DepartmentCreateCommandHandler(IDbContext dbContext, ICounterService counterService)
        {
            Ensure.NotNull(dbContext, nameof(dbContext));
            Ensure.NotNull(counterService, nameof(counterService));

            _dbContext = dbContext;
            _counterService = counterService;
        }

        public override async Task<int> HandleAsync(DepartmentCreateCommand command, CancellationToken cancellationToken)
        {
            Department? parent = null;
            if (command.ParentId != null)
            {
                parent = await _dbContext.Departments
                    .Where(x => x.Id == command.ParentId)
                    .SingleOrDefaultAsync(cancellationToken);
                if (parent == null)
                {
                    throw new InvalidOperationException("Parent department not found.");
                }
            }

            int id = await _counterService.GetValueAsync("DepartmentId", cancellationToken);
            HierarchyId hierarchyId;
            if (parent != null)
            {
                HierarchyId parentHierarchyId = HierarchyId.Parse(parent.HierarchyId);
                hierarchyId = parentHierarchyId.GetDescendant(id);
            }
            else
            {
                hierarchyId = new HierarchyId(new[] { id });
            }

            var department = new Department(id,
                hierarchyId.ToString(),
                parent,
                command.Name);

            _dbContext.Departments.Add(department);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return department.Id;
        }
    }
}
