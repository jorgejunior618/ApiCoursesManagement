using curso.api.Busines.Entities;
using curso.api.Busines.Repositories;

namespace curso.api.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;

        public UserRepository(CourseDbContext context)
        {
            this._context = context;
        }

        public void Add(User user)
        {
            _context.User.Add(user);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public User GetUser(string login)
        {
            _context.User.FirstOrDefault(user => user.Login == login)
        }
    }
}
