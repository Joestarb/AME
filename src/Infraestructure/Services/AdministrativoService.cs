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
    public class AdministrativoService : IAdministrativoService
    {
        private readonly ApplicationDbContext _context;

        public AdministrativoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Administrativo> GetAdministrativoByColaboradorIdAsync(int colaboradorId)
        {
            return await _context.Administrativo
                                 .FirstOrDefaultAsync(a => a.FKColaborador == colaboradorId);
        }

        public async Task AddAdministrativoAsync(Administrativo administrativo)
        {
            _context.Administrativo.Add(administrativo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdministrativoAsync(Administrativo administrativo)
        {
            _context.Administrativo.Update(administrativo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdministrativoAsync(Administrativo administrativo)
        {
            _context.Administrativo.Remove(administrativo);
            await _context.SaveChangesAsync();
        }
    }

}
