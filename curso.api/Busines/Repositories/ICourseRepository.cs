using curso.api.Busines.Entities;

namespace curso.api.Busines.Repositories
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Commit();
        IList<Course> GetByUser(int userId);
    }
}
