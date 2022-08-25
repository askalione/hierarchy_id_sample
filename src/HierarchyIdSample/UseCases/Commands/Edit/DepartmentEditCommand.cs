namespace HierarchyIdSample.UseCases.Commands.Edit
{
    public record DepartmentEditCommand : IOperation
    {
        public int Id { get; init; }
        public int? ParentId { get; init; }
        public string Name { get; init; } = default!;
    }
}
