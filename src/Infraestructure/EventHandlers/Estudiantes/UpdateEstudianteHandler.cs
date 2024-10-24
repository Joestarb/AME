using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.EventHandlers.Estudiantes
{
    public class UpdateEstudianteHandler : IRequestHandler<UpdateEstudianteCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateEstudianteHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(UpdateEstudianteCommand command, CancellationToken cancellationToken)
        {
            var estudiante = await _context.Estudiantes.FindAsync(command.Id);

            if (estudiante == null)
            {
                return new Response<int>($"Estudiante con Id {command.Id} no encontrado.");
            }

            estudiante.Nombre = command.Nombre;
            estudiante.Edad = command.Edad;
            estudiante.Correo = command.Correo;

            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<int>(estudiante.Id, "Actualización exitosa.");
        }
    }
}

