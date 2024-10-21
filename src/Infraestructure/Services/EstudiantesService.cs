using ApplicationCore.Interfaces;
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
    }
}
