using Laboratorio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Entities
{
    public class ExamenDTO
    {

        public int IdExamen { get; set; }
        public string Codigo {  get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set;}
        public int IdMuestrafk {  get; set; }
        public string TipoMuestra { get; set; }
        public int IdPacientefk { get; set; }

    }
}
