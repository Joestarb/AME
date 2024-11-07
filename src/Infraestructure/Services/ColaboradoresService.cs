using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using DevExpress.XtraRichEdit.Import.Html;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Services
{
    public class ColaboradoresService: IColaboradoresService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ColaboradoresService(ApplicationDbContext dbContext, IMapper mapper, IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetColaboradores()
        {
            object list = new object();
            list = await _dbContext.Colaboradores.ToListAsync();
            return new Response<object>(list);
        }

        //public async Task<IEnumerable<Colaboradores>> GetByFechaCreacionAsync(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    return await _dbContext.Set<Colaboradores>()
        //        .Where(c => c.FechaCreacion >= fechaInicio && c.FechaCreacion <= fechaFin)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Colaboradores>> GetByIsProfesorAsync(bool isProfesor)
        //{
        //    return await _dbContext.Set<Colaboradores>()
        //        .Where(c => c.IsProfesor == isProfesor)
        //    .ToListAsync();
        //}


        public async Task<IEnumerable<Colaboradores>> GetColaboradoresFilteredAsync(DateTime? fechaInicio, DateTime? fechaFin, bool? isProfesor, int? edad)
        {
            var query = _dbContext.Colaboradores.AsQueryable();

            if (fechaInicio.HasValue)
            {
                query = query.Where(c => c.FechaCreacion >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(c => c.FechaCreacion <= fechaFin.Value);
            }

            if (isProfesor.HasValue)
            {
                query = query.Where(c => c.IsProfesor == isProfesor.Value);
            }

            if (edad.HasValue)
            {
                query = query.Where(c => c.Edad == edad.Value);
            }

            // Log de la consulta generada
            var sql = query.ToQueryString(); // Solo disponible en EF Core 5 o superior
            Console.WriteLine($"Consulta SQL: {sql}");

            return await query.ToListAsync();
        }

        public async Task<ColaboradorDTO> CreateAsync(ColaboradoresCreateCommand command)
        {
            // Mapea el comando a la entidad
            var colaborador = _mapper.Map<Colaboradores>(command);

            // Añade la entidad al contexto
            _dbContext.Set<Colaboradores>().Add(colaborador);
            await _dbContext.SaveChangesAsync();

            // Devuelve el colaborador creado como DTO
            return _mapper.Map<ColaboradorDTO>(colaborador);
        }

        public async Task<bool> UpdateColaboradorAsync(Colaboradores colaborador)
        {
            var existingColaborador = await _dbContext.Colaboradores.FindAsync(colaborador.Id);
            if (existingColaborador == null) return false;

            existingColaborador.Nombre = colaborador.Nombre;
            existingColaborador.Edad = colaborador.Edad;
            existingColaborador.BirthDate = colaborador.BirthDate;
            existingColaborador.IsProfesor = colaborador.IsProfesor;
            existingColaborador.FechaCreacion = colaborador.FechaCreacion;

            _dbContext.Colaboradores.Update(existingColaborador);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteColaboradorAsync(int id)
        {
            // Lógica de eliminación del colaborador
            var colaborador = await _dbContext.Colaboradores.FindAsync(id);
            if (colaborador == null) return false;

            _dbContext.Colaboradores.Remove(colaborador);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}