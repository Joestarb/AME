using ApplicationCore.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly ApplicationDbContext _context;

        public ProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profesor> GetProfesorByColaboradorIdAsync(int colaboradorId)
        {
            return await _context.Profesor
                                 .FirstOrDefaultAsync(p => p.FKColaborador == colaboradorId);
        }

        public async Task AddProfesorAsync(Profesor profesor)
        {
            _context.Profesor.Add(profesor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfesorAsync(Profesor profesor)
        {
            _context.Profesor.Update(profesor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesorAsync(Profesor profesor)
        {
            _context.Profesor.Remove(profesor);
            await _context.SaveChangesAsync();
        }
    }

}
