using curso.api.Busines.Entities;
using curso.api.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infrastructure.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
    }
}
