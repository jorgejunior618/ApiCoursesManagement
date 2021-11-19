using curso.api.Busines.Entities;
using curso.api.Busines.Repositories;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infrastructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        readonly CourseDbContext _context;

        public CourseRepository(CourseDbContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Course.Add(course);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Course> GetByUser(int userId)
        {
            return _context.Course
                .Include(course => course.User)
                .Where(course => course.UserCode == userId)
                .ToList();
        }
    }
}
