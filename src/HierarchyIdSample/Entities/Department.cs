namespace HierarchyIdSample.Entities
{
    public class Department
    {
        public int Id { get; private set; }

        public string HierarchyId { get; private set; } = default!;

        public int? ParentId { get; private set; }
        public virtual Department? Parent { get; private set; }

        public string Name { get; private set; } = default!;

        private Department() { }

        public Department(int id,
            string hierarchyId,
            Department? parent,
            string name) : this()
        {
            Ensure.NotEmpty(hierarchyId, nameof(hierarchyId));
            Ensure.NotEmpty(name, nameof(name));

            Id = id;
            HierarchyId = hierarchyId;

            ParentId = parent?.Id;
            Parent = parent;

            Name = name;
        }

        public void Edit(string name)
        {
            Ensure.NotEmpty(name, nameof(name));

            Name = name;
        }

        public void SetParent(string hierarchyId, Department? parent)
        {
            Ensure.NotEmpty(hierarchyId, nameof(hierarchyId));

            HierarchyId = hierarchyId;

            ParentId = parent?.Id;
            Parent = parent;
        }
    }
}
