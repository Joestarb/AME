using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradoresService _service;
        private readonly IMediator _mediator;

        public ColaboradoresController(IColaboradoresService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        // Lista de usuarios - GET
        [HttpGet("getColaboradores")]
        public async Task<IActionResult> GetColaboradores()
        {
            var result = await _service.GetColaboradores();
            return Ok(result);
        }

        // Crear estudiante - POST
        [HttpPost("createColaboradores")]
        public async Task<ActionResult<Response<int>>> CreateColaboradores(ColaboradoresCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredColaboradores([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] bool? isProfesor)
        {
            // Llama al método de la instancia _colaboradoresService
            var colaboradores = await _service.GetColaboradoresFilteredAsync(fechaInicio, fechaFin, isProfesor);

            return Ok(colaboradores);
        }
    }
}
