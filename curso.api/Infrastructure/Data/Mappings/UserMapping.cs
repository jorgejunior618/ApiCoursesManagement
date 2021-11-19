using curso.api.Busines.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso.api.Infrastructure.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TBL_USER");
            builder.HasKey(user => user.Code);
            builder.Property(user => user.Code).ValueGeneratedOnAdd();
            builder.Property(user => user.Email);
            builder.Property(user => user.Login);
            builder.Property(user => user.Password);
        }
    }
}
