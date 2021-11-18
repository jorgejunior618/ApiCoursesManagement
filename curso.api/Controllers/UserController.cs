using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
            if(!ModelState.IsValid)
            {
                return BadRequest(
                    new FieldValidatorViewModelOutput(
                        ModelState.SelectMany(selected => selected.Value.Errors)
                        .Select(s => s.ErrorMessage)
                    )
                );
            }
            return Ok(loginViewModelInput);
        }

        [HttpPost]
        [Route("Registrar")]
        [CustomModelStateValidation]
        public IActionResult Registrar(RegisterViewModelInput registerViewModelInput)
        {
            return Created("", registerViewModelInput);
        }
    }
}
