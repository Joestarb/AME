using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Handlers
{
    public class ColaboradoresCreateCommandHandler : IRequestHandler<ColaboradoresCreateCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;

        public ColaboradoresCreateCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(ColaboradoresCreateCommand request, CancellationToken cancellationToken)
        {
            var colaborador = new Colaboradores
            {
                Nombre = request.Nombre,
                Edad = request.Edad,
                BirthDate = request.BirthDate,
                IsProfesor = request.IsProfesor,
                FechaCreacion = request.FechaCreacion
            };

            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.IsProfesor == true)
            {
                var profesor = new Profesor
                {
                    FKColaborador = colaborador.Id,
                    Correo = request.Correo,
                    Departamento = request.Departamento
                };

                _context.Profesor.Add(profesor);
            }
            else
            {
                var administrativo = new Administrativo
                {
                    FKColaborador = colaborador.Id,
                    Correo = request.Correo,
                    Puesto = request.Puesto,
                    Nomina = request.Nomina
                };

                _context.Administrativo.Add(administrativo);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Response<int>(colaborador.Id);
        }


    }
}