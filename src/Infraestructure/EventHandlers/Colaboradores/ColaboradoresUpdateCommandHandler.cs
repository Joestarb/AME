using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestructure.EventHandlers.Colaboradores
{
    public class ColaboradoresUpdateCommandHandler : IRequestHandler<ColaboradoresUpdateCommand, bool>
    {
        private readonly IColaboradoresService _colaboradoresService;
        private readonly IProfesorService _profesorService;
        private readonly IAdministrativoService _administrativoService;

        public ColaboradoresUpdateCommandHandler(IColaboradoresService colaboradoresService,
                                                  IProfesorService profesorService,
                                                  IAdministrativoService administrativoService)
        {
            _colaboradoresService = colaboradoresService;
            _profesorService = profesorService;
            _administrativoService = administrativoService;
        }

        public async Task<bool> Handle(ColaboradoresUpdateCommand request, CancellationToken cancellationToken)
        {
            // Verificar que request.Id no sea nulo, o asignar un valor por defecto
            int colaboradorId = request.Id ?? 0;  // Asume 0 si es nulo, ajusta según tu lógica

            // Asignar valores predeterminados si son nulos
            DateTime birthDate = request.BirthDate ?? DateTime.MinValue;  // Si es null, se asigna DateTime.MinValue
            DateTime fechaCreacion = request.FechaCreacion;  // Si es null, se asigna DateTime.MinValue

            // Asignar valor predeterminado a Edad si es nulo
            int edad = request.Edad;  // Si es null, se asigna 0

            // Actualizar el colaborador en la tabla Colaboradores
            var colaborador = new Domain.Entities.Colaboradores
            {
                Id = colaboradorId,
                Nombre = request.Nombre,
                Edad = edad,
                BirthDate = birthDate,
                IsProfesor = request.IsProfesor,
                FechaCreacion = fechaCreacion
            };

            // Actualizar el colaborador en la base de datos
            bool updated = await _colaboradoresService.UpdateColaboradorAsync(colaborador);

            if (!updated)
            {
                return false;
            }

            // Si el colaborador es ahora Profesor
            if (request.IsProfesor)
            {
                // Eliminar de Administrativo si existe
                var administrativo = await _administrativoService.GetAdministrativoByColaboradorIdAsync(colaboradorId);
                if (administrativo != null)
                {
                    await _administrativoService.DeleteAdministrativoAsync(administrativo);
                }

                // Añadir o actualizar en la tabla de Profesores
                var profesor = await _profesorService.GetProfesorByColaboradorIdAsync(colaboradorId);
                if (profesor == null)
                {
                    // Si no existe, agregar nuevo Profesor
                    var newProfesor = new Domain.Entities.Profesor
                    {
                        FKColaborador = colaboradorId,
                        Correo = request.Correo,
                        Departamento = request.Departamento
                    };
                    await _profesorService.AddProfesorAsync(newProfesor);
                }
                else
                {
                    // Si ya existe, actualizar los datos
                    profesor.Correo = request.Correo;
                    profesor.Departamento = request.Departamento;
                    await _profesorService.UpdateProfesorAsync(profesor);
                }
            }
            else
            {
                // Eliminar de Profesor si existe
                var profesor = await _profesorService.GetProfesorByColaboradorIdAsync(colaboradorId);
                if (profesor != null)
                {
                    await _profesorService.DeleteProfesorAsync(profesor);
                }

                // Añadir o actualizar en la tabla de Administrativos
                var administrativo = await _administrativoService.GetAdministrativoByColaboradorIdAsync(colaboradorId);
                if (administrativo == null)
                {
                    // Si no existe, agregar nuevo Administrativo
                    var newAdministrativo = new Domain.Entities.Administrativo
                    {
                        FKColaborador = colaboradorId,
                        Correo = request.Correo,
                        Puesto = request.Puesto,
                        Nomina = request.Nomina
                    };
                    await _administrativoService.AddAdministrativoAsync(newAdministrativo);
                }
                else
                {
                    // Si ya existe, actualizar los datos
                    administrativo.Correo = request.Correo;
                    administrativo.Puesto = request.Puesto;
                    administrativo.Nomina = request.Nomina;
                    await _administrativoService.UpdateAdministrativoAsync(administrativo);
                }
            }

            return true;
        }

    }
}
