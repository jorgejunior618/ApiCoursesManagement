using curso.api.Models.Users;

namespace curso.api.Configurations
{
    public interface IAuthenticationService
    {
        object GetToken(UserViewModelOutput userViewModelOutput);
    }
}
