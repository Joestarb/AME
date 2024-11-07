using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    [Table("Administrativo")]
    public class Administrativo
    {
        public int Id { get; set; }
        public int FKColaborador {  get; set; }
        public string Correo {  get; set; }
        public string Puesto { get; set; }
        public string Nomina { get; set; }
    }
}
