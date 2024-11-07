using ApplicationCore.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IColaboradoresService
    {
        Task<Response<object>> GetColaboradores();
        Task<IEnumerable<Colaboradores>> GetColaboradoresFilteredAsync(DateTime? fechaInicio, DateTime? fechaFin, bool? isProfesor);

    }
}
