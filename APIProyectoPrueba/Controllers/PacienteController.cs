using Laboratorio.Core.Interfaces.IPaciente;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Core.Entities;

namespace APILaboratorio.Controllers
{
   
    public class PacienteController : ControllerBase
    {
        public readonly IPaciente servicePaciente;
        public ResponseService rs = new ResponseService();

        //constructor
        public PacienteController(IPaciente _servicePaciente)
        {
            servicePaciente = _servicePaciente;
        }

        [HttpGet]
        [Route("Paciente")]

        public async Task<IActionResult> GetPaciente()
        {
            var rService = await servicePaciente.GetPaciente(); 
            return Ok(rService);
        }
    }
}
