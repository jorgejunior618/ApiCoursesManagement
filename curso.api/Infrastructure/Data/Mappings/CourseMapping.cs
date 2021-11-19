using curso.api.Busines.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso.api.Infrastructure.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("TBL_COURSE");
            builder.HasKey(course => course.Code);
            builder.Property(course => course.Code).ValueGeneratedOnAdd();
            builder.Property(course => course.Name);
            builder.Property(course => course.Description);
            builder.HasOne(course => course.User)
                .WithMany()
                .HasForeignKey(foreignKey => foreignKey.UserCode);
        }
    }
}
