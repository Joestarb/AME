using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.EventHandlers.Estudiantes
{
    public class DeleteEstudianteHandler : IRequestHandler<DeleteEstudianteCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteEstudianteHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(DeleteEstudianteCommand command, CancellationToken cancellationToken)
        {
            var estudiante = await _context.Estudiantes.FindAsync(command.Id);

            if (estudiante == null)
            {
                return new Response<int>($"Estudiante con Id {command.Id} no encontrado.");
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<int>(estudiante.Id, "Eliminación exitosa.");
        }
    }
}
