using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio.Core.Entities
{
    public class PacienteDTO
    {
        public int IdPaciente { get; set; }
        public string NumDoc { get; set; }
        public string NombrePac { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaCump { get; set; }
        public int Edad { get; set; } 
        public int IdTdFk { get; set; }
        public string TipoDoc {  get; set; }     

    }
}
