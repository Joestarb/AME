using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;

namespace Infraestructure.EventHandlers.Estudiantes
{
    public class CreateEstudiantesHandler : IRequestHandler<EstudianteCreateCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEstudiantesService _service;

        public CreateEstudiantesHandler(ApplicationDbContext context, IMapper mapper, IEstudiantesService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<Response<int>> Handle (EstudianteCreateCommand command, CancellationToken cancellationToken)
        {
            var e = new EstudianteCreateCommand();
            e.Nombre = command.Nombre;
            e.Edad = command.Edad;
            e.Correo = command.Correo;

            var es = _mapper.Map<Domain.Entities.Estudiantes>(e);
            await _context.AddAsync(es);
            var req = await _context.SaveChangesAsync(cancellationToken);
            return new Response<int>(es.Id, "Registro exitoso");
        }
    }
}
