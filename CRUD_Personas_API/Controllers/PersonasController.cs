using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_Personas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        // GET: api/<PersonasController>
        [HttpGet]
        public IEnumerable<ClsPersona> Get()
        {
            return ClsListadoPersonasBL.getListadoPersonasCompletoBL(); ;
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public ClsPersona Get(int id)
        {
            return ClsListadoPersonasBL.getPersonaBL(id);
        }

        // POST api/<PersonasController>
        [HttpPost]
        public void Post([FromBody] ClsPersona oPersona)
        {
            ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ClsPersona oPersona)
        {
            ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ClsManejadoraPersonaBL.eliminarPersonaBL(id);
        }
    }
}
