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

        // Lista de usuarios - GET
        [HttpGet("getEstudiantes")]
        public async Task<IActionResult> GetEstudiantes()
        {
            var result = await _service.GetEstudiantes();
            return Ok(result);
        }

        // Obtener estudiante por ID - GET
        [HttpGet("getEstudiante/{id}")]
        public async Task<IActionResult> GetEstudiante(int id)
        {
            var estudiante = await _service.GetEstudianteById(id);
            if (estudiante == null)
                return NotFound();

            return Ok(new { succeeded = true, result = estudiante });
        }


        // Crear estudiante - POST
        [HttpPost("createEstudiante")]
        public async Task<ActionResult<Response<int>>> CreateEstudiantes(EstudianteCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // Actualizar estudiante - PUT
        [HttpPut("updateEstudiante/{id}")]
        public async Task<ActionResult<Response<int>>> UpdateEstudiante(int id, UpdateEstudianteCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID no coincide con el estudiante");

            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return NotFound(result.Message);

            return Ok(result);
        }

        // Eliminar estudiante - DELETE
        [HttpDelete("deleteEstudiante/{id}")]
        public async Task<ActionResult<Response<int>>> DeleteEstudiante(int id)
        {
            var result = await _mediator.Send(new DeleteEstudianteCommand { Id = id });
            if (!result.Succeeded)
                return NotFound(result.Message);

            return Ok(result);
        }


    }
}
