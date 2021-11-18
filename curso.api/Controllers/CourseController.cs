using curso.api.Models;
using curso.api.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace curso.api.Controllers
{
    [Route("api/V1/Courses")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        
        /// <summary>
        /// O serviço permite adicionar cursos ao usuário cadastrado
        /// </summary>
        /// <param name="courseViewModelInput"> View Model do Login</param>
        /// <returns>Retorna Created se os dados informados o tokem estiverem corretos</returns>
        [SwaggerResponse(statusCode: 201, description: "Curso cadastrado com sucesso", Type = typeof(CourseViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [SwaggerResponse(statusCode: 500, description: "Erro nos servidores, tente novamente mais tarde", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Create(CourseViewModelInput courseViewModelInput)
        {
            var userCode = int.Parse(
                User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? "0"
            );
            return Created("", courseViewModelInput);
        }

        /// <summary>
        /// O serviço permite listar cursos do usuário cadastrado
        /// </summary>
        /// <returns>Retorna Ok se o token estiver correto</returns>
        [SwaggerResponse(statusCode: 200, description: "Ok", Type = typeof(CourseViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [SwaggerResponse(statusCode: 500, description: "Erro nos servidores, tente novamente mais tarde", Type = typeof(GenericErrorViewModel))]
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> Get()
        {
            var courses = new List<CourseViewModelOutput>();
            var userCode = int.Parse(
                User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? "0"
            );

            courses.Add(new CourseViewModelOutput() {
                Login = userCode.ToString(),
                Name = "DotNet",
                Description = "teste"
            });

            return Ok(courses);
        }
    }
}
