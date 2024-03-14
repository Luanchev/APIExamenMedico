using Proyecto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio.Core.Interfaces.IPaciente
{
    public interface IPaciente
    {
        Task<ResponseService> GetPaciente();

    }
}
