using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IProfesorService
    {
        Task<Profesor> GetProfesorByColaboradorIdAsync(int colaboradorId);
        Task AddProfesorAsync(Profesor profesor);
        Task UpdateProfesorAsync(Profesor profesor);
        Task DeleteProfesorAsync(Profesor profesor);
    }

}
