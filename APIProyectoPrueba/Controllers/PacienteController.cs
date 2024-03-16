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

        [HttpGet]
        [Route("Paciente/NumeroDocumento/{NumDoc}")]
        public async Task<IActionResult> GetPacienteByDocument([FromRoute] string NumDoc)
        {
            var rService = await servicePaciente.GetPacienteByDocument(NumDoc);

            if (rService.Status == 400)
            {
                return NotFound(rService);
            }
            else if (rService.Status == 200)
            {
                return Ok(rService);
            }
            else
            {
                return BadRequest(rService);
            }
        }
    }
}
