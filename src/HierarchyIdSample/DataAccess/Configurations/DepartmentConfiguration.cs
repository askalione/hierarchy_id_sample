using HierarchyIdSample.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HierarchyIdSample.DataAccess.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(x => x.Parent)
                .WithMany()
                .HasForeignKey(x => x.ParentId);

            builder.ToTable("departments");
        }
    }
}
