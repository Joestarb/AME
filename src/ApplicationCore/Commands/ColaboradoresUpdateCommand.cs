using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands
{
    public class ColaboradoresUpdateCommand : IRequest<bool>
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime?BirthDate { get; set; }
        public bool IsProfesor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Correo {  get; set; }
        public string? Departamento { get; set; }
        public string? Puesto { get; set; }
        public string? Nomina { get; set; }
    }
}
