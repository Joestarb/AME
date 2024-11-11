using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappings;
using ApplicationCore.Wrappers;
using AutoMapper;

using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public class EstudiantesService : IEstudiantesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EstudiantesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetEstudiantes()
        {
            object list = new object();
            list = await _dbContext.Estudiantes.ToListAsync();
            return new Response<object>(list);
        }

        public async Task<Response<Estudiantes>> GetEstudianteById(int id)
        {
            var estudiante = await _dbContext.Estudiantes.FindAsync(id);

            // Verificamos si se encontró al estudiante
            if (estudiante == null)
                return new Response<Estudiantes>("Estudiante no encontrado"); // Solo se pasa el mensaje

            return new Response<Estudiantes>(estudiante); // Se retorna solo el estudiante encontrado
        }

        public async Task<Response<int>> UpdateEstudiante(UpdateEstudianteCommand command)
        {
            var estudiante = await _dbContext.Estudiantes.FindAsync(command.Id);

            // Verificamos si se encontró al estudiante
            if (estudiante == null)
                return new Response<int>("Estudiante no encontrado"); // Solo se pasa el mensaje

            // Actualizar las propiedades del estudiante
            estudiante.Nombre = command.Nombre;
            estudiante.Edad = command.Edad;
            estudiante.Correo = command.Correo;

            _dbContext.Estudiantes.Update(estudiante);
            await _dbContext.SaveChangesAsync();

            return new Response<int>(estudiante.Id); // Se retorna el ID del estudiante actualizado
        }



    }
}
