using curso.api.Busines.Entities;
using curso.api.Filters;
using curso.api.Infrastructure.Data;
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
            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = 1,
                Login = "JorgeJun",
                Email = "jorgejun@email.com"
            };

            var secret = Encoding.ASCII.GetBytes("eyJsb2dpbiI6InRlc3RlIiwic2VuaGEiOiJ0ZXN0ZTEyMyJ9");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                User= userViewModelOutput
            });
        }

        [HttpPost]
        [Route("Registrar")]
        [CustomModelStateValidation]
        public IActionResult Registrar(RegisterViewModelInput registerViewModelInput)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=CURSO;user=sa;password=App@223020");

            CourseDbContext context = new CourseDbContext(optionsBuilder.Options);

            var pendingMigrations = context.Database.GetPendingMigrations();

            if( pendingMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }

            var user = new User();

            user.Email = registerViewModelInput.Email;
            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;

            context.User.Add(user);
            context.SaveChanges();

            return Created("", registerViewModelInput);
        }
    }
}
