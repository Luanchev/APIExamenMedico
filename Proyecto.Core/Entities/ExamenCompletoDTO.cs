
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio.Core.Entities
{
    public class ExamenCompletoDTO
    {
        //caracteristicas de examen
        public int IdExamen { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int IdMuestrafk { get; set; }
        public string TipoMuestra { get; set; }
        public int IdPacientefk { get; set; }

        //caracteristicas de paciente

        public int IdPaciente { get; set; }
        public string NumDoc { get; set; }
        public string NombrePac { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaCump { get; set; }
        public int Edad { get; set; }
        public int IdTdFk { get; set; }
        public string TipoDoc { get; set; }
        
    }
}
