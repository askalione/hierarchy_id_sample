using HierarchyIdSample.Entities;

namespace HierarchyIdSample.UseCases.Commands.Edit
{
    internal class DepartmentEditCommandHandler : AsyncOperationHandler<DepartmentEditCommand>
    {
        private readonly IDbContext _dbContext;

        public DepartmentEditCommandHandler(IDbContext dbContext)
        {
            Ensure.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }
        protected override async Task HandleAsync(DepartmentEditCommand command, CancellationToken cancellationToken)
        {
            Department? department = await _dbContext.Departments
                .Include(x => x.Parent)
                .Where(x => x.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);
            if (department == null)
            {
                throw new InvalidOperationException("Department not found.");
            }

            HierarchyId hierarchyId = HierarchyId.Parse(department.HierarchyId);

            Department? parent = department.Parent;
            if (command.ParentId != null)
            {
                parent = await _dbContext.Departments
                    .Where(x => x.Id == command.ParentId)
                    .SingleOrDefaultAsync(cancellationToken);
                if (parent == null)
                {
                    throw new InvalidOperationException("Parent department not found.");
                }

                HierarchyId parentHierarchyId = HierarchyId.Parse(parent.HierarchyId);
                if (parentHierarchyId.IsDescendantOf(department.Id))
                {
                    throw new InvalidOperationException(
                        "Specified parent department is descendant of current department.");
                }

                hierarchyId = parentHierarchyId.GetDescendant(department.Id);
            }
            else
            {
                hierarchyId = new HierarchyId(new[] { department.Id });
            }

            department.Edit(command.Name);
            department.SetParent(hierarchyId.ToString(), parent);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
