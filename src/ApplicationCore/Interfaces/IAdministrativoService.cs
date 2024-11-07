using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdministrativoService
    {
        Task<Administrativo> GetAdministrativoByColaboradorIdAsync(int colaboradorId);
        Task AddAdministrativoAsync(Administrativo administrativo);
        Task UpdateAdministrativoAsync(Administrativo administrativo);
        Task DeleteAdministrativoAsync(Administrativo administrativo);
    }

}
