using curso.api.Busines.Entities;

namespace curso.api.Busines.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        User GetUser(string login);
    }
}
