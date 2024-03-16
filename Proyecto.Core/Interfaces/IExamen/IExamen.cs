using Laboratorio.Core.Entities;
using Proyecto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio.Core.Interfaces.IExamen
{
    public interface IExamen
    {
        Task<ResponseService> GetExamen();
        Task<ResponseService> GetExamenByKey(string codigo);
        Task<ResponseService> GetExamenByDP(string numdoc);
        Task<ResponseService> CreateExam(ExamenDTO examen);
        Task<ResponseService> EditExam(ExamenDTO examen);
        Task<ResponseService> DeleteExam(int IdExamen);
       
    }

    
}
