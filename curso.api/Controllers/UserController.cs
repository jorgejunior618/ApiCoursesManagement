using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using curso.api.Models.Users;

namespace curso.api.Controllers
{
    [Route("api/V1/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Create(LoginViewModelInput loginViewModelInput)
        {
            return Ok(loginViewModelInput);
        }

        [HttpPost]
        [Route("Registrar")]
        public IActionResult Registrar(RegisterViewModelInput registerViewModelInput)
        {
            return Created("", registerViewModelInput);
        }
    }
}
