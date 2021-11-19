using curso.api.Busines.Entities;
using curso.api.Busines.Repositories;
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
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

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
            Course course = new Course();

            var userCode = int.Parse(
                User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? "0"
            );
            course.Name = courseViewModelInput.Name;
            course.Description = courseViewModelInput.Description;
            course.UserCode = userCode;

            _courseRepository.Add(course);
            _courseRepository.Commit();

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
            var userCode = int.Parse(
                User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? "0"
            );

            var courses = _courseRepository.GetByUser(userCode)
                .Select(course => new CourseViewModelOutput()
                {
                    Name = course.Name,
                    Description = course.Description,
                    Login = course.User.Login
                });

            return Ok(courses);
        }
    }
}
