using curso.api.Busines.Entities;
using curso.api.Busines.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Infrastructure.Data;
using curso.api.Infrastructure.Data.Repositories;
using curso.api.Models;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace curso.api.Controllers
{
    [Route("api/V1/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IAuthenticationService _authentication;

        public UserController(
            IUserRepository userRepository,
            IAuthenticationService authentication
        )
        {
            _userRepository = userRepository;
            _authentication = authentication;
        }

        /// <summary>
        /// O serviço permite autenticar os usuários cadastrados
        /// </summary>
        /// <param name="loginViewModelInput"> View Model do Login</param>
        /// <returns>Retorna Ok se as credenciais enviadas estiverem corretas</returns>
        [SwaggerResponse(statusCode: 200, description: "Autenticação bem sucedida", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Login ou senha inválida", Type = typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro nos servidores, tente novamente mais tarde", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("Login")]
        [CustomModelStateValidation]
        public IActionResult Create(LoginViewModelInput loginViewModelInput)
        {
            User user = _userRepository.GetUser(loginViewModelInput.Login);

            if (user == null)
            {
                return BadRequest("Erro ao tentar Logar");
            }
            //if (user.Password != loginViewModelInput.Password.)
            //{

            //}
            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = user.Code,
                Login = user.Login,
                Email = user.Email
            };

            var token = _authentication.GetToken(userViewModelOutput);

            return Ok(new
            {
                Token = token,
                User= userViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço realiza o cadastro de um novo usuário no banco de dados
        /// </summary>
        /// <param name="registerViewModelInput">View model de Cadastro</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Autenticação bem sucedida", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Login ou senha inválida", Type = typeof(FieldValidatorViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro nos servidores, tente novamente mais tarde", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("Registrar")]
        [CustomModelStateValidation]
        public IActionResult Registrar(RegisterViewModelInput registerViewModelInput)
        {
            

            //var pendingMigrations = context.Database.GetPendingMigrations();

            /*if( pendingMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }*/

            var user = new User();

            user.Email = registerViewModelInput.Email;
            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;


            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", registerViewModelInput);
        }
    }
}
