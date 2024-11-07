using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappings;
using ApplicationCore.Wrappers;
using AutoMapper;
using DevExpress.DataAccess.ObjectBinding;
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

        public async Task<byte[]> GetPDF()
        {
            var estudiantes = await _dbContext.Estudiantes
                .Select(e => new EstudianteDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Edad = e.Edad,
                    Correo = e.Correo
                })
                .ToListAsync();

            var reportePdf = new EstudiantesDTOPDF
            {
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                Hora = DateTime.Now.ToString("HH:mm"),
                Estudiantes = estudiantes
            };

            // Configura el ObjectDataSource
            ObjectDataSource source = new ObjectDataSource { DataSource = reportePdf };

            // Asigna DataSource y DataMember
            var report = new ApplicationCore.PDF.EstudiantesPDF
            {
                DataSource = source,
                DataMember = "Estudiantes"  // Indicamos que los datos están en la propiedad "Estudiantes"
            };

            using (var memory = new MemoryStream())
            {
                await report.ExportToPdfAsync(memory);
                return memory.ToArray();
            }
        }


    }
}
