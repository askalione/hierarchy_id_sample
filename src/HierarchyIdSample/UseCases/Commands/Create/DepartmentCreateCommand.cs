namespace HierarchyIdSample.UseCases.Commands.Create
{
    public record DepartmentCreateCommand : IOperation<int>
    {
        public int? ParentId { get; init; }
        public string Name { get; init; } = default!;
    }
}
