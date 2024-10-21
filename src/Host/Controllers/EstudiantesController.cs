
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ApplicationCore.Wrappers;
using ApplicationCore.Commands;

namespace Host.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesService _service;
        private readonly IMediator _mediator;
        public EstudiantesController(IEstudiantesService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        //<sumary>
        //Lista de usuarios - Get
        //</sumary>

        [Route("getEstudiantes")]
        [HttpGet]

        public async Task<IActionResult> GetEstudiantes()
        {
            var result = await _service.GetEstudiantes();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> CreateEstudiantes(EstudianteCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
