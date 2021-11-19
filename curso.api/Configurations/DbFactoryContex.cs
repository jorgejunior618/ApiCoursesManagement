using curso.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
    /*
        Add-Migration
        Bundle-Migration
        Drop-Database
        Get-DbContext
        Get-Migration
        Optimize-DbContext
        Remove-Migration
        Scaffold-DbContext
        Script-Migration
        Update-Database 
    */
    public class DbFactoryContex : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=CURSO;user=sa;password=App@223020");

            CourseDbContext context = new CourseDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
