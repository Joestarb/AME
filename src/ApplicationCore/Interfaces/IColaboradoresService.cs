using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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
        Task<IEnumerable<Colaboradores>> GetColaboradoresFilteredAsync(DateTime? fechaInicio, DateTime? fechaFin, bool? isProfesor, int? edad);
        Task<bool> UpdateColaboradorAsync(Colaboradores colaborador);
        Task<bool> DeleteColaboradorAsync(int id);
    }
}
