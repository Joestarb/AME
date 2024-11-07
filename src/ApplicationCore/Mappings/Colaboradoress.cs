using ApplicationCore.Commands;
using ApplicationCore.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappings
{
    public class Colaboradoress : Profile
    {
        public Colaboradoress()
        {
            CreateMap<ColaboradoresCreateCommand, Colaboradores>()
                .ForMember(x => x.Id, y => y.Ignore());

            // Mapeo de entidad a DTO para lectura
            CreateMap<Colaboradores, ColaboradorDTO>().ReverseMap();
        }

    }
}
