using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Estudiantes")]
    public class Estudiantes
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad {  get; set; }
        public string Correo {  get; set; }
    }
}
