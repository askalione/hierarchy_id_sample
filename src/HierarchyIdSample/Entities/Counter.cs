namespace HierarchyIdSample.Entities
{
    public class Counter
    {
        public string Name { get; private set; } = default!;
        public int Value { get; private set; }

        private Counter() { }

        public Counter(string name, int value) : this()
        {
            Ensure.NotEmpty(name, nameof(name));

            Name = name;
            Value = value;
        }
    }
}
