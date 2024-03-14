using Laboratorio.Core.Interfaces.IExamen;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Proyecto.Core.Entities;

namespace APIProyectoPrueba.Controllers
{

    [Route("api")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        public readonly IExamen serviceExamen;
        public ResponseService rs = new ResponseService();

        //constructor
        public ExamenController(IExamen _serviceExamen)
        {
            serviceExamen = _serviceExamen;
        }

        [HttpGet]
        [Route("Examen")]
        public async Task<IActionResult> GetExamen()
        {
            var rService = await serviceExamen.GetExamen();

            return Ok(rService);            
        }


        [HttpGet]
        [Route("Examen/codigo/{codigo}")]
        public async Task<IActionResult> GetExamenByKey([FromRoute] string codigo)
        {
            var rService = await serviceExamen.GetExamenByKey(codigo);

            if(rService.Status == 400)
            {
                return NotFound(rService);
            }else if(rService.Status == 200)
            {
                return Ok(rService);
            }
            else
            {
                return BadRequest(rService);
            }
        }


        [HttpGet]
        [Route("Examen/DocPac/{numdoc}")]
        public async Task<IActionResult> GetExamenByDP([FromRoute] string numdoc)
        {
            var rService = await serviceExamen.GetExamenByDP(numdoc);

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



        [HttpPost]
        [Route("examen")]
        public async Task<IActionResult> CreateExam([FromBody] ExamenDTO examen) //frombody esta indicando los parametros que recibe
        {
            var rService = await serviceExamen.CreateExam(examen);
            if (rService.Status == 400)
            {
                return NotFound(rService); //retorna el estatus 
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



        [HttpPut]
        [Route("examen")]

        public async Task<IActionResult> EditExam([FromBody] ExamenDTO examen) //frombody esta indicando los parametros que recibe
        {
            var rService = await serviceExamen.EditExam(examen);
            if (rService.Status == 400)
            {
                return NotFound(rService); //retorna el estatus 
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


        [HttpDelete]
        [Route("camion/{IdExamen}")]

        public async Task<IActionResult> DeleteExam(int IdExamen)
        {
            var rService = await serviceExamen.DeleteExam(IdExamen);
            if (rService.Status == 400)
            {
                return NotFound(rService); //retorna el estatus 
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
