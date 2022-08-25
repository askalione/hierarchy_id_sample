using HierarchyIdSample.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HierarchyIdSample.DataAccess.Configurations
{
    internal class CounterConfiguration : IEntityTypeConfiguration<Counter>
    {
        public void Configure(EntityTypeBuilder<Counter> builder)
        {
            builder.HasKey(x => x.Name);
            builder.Property(x => x.Name)
                .ValueGeneratedNever();

            builder.ToTable("counters");
        }
    }
}
