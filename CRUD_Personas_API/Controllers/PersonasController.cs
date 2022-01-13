using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

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
            List<ClsPersona> listaPersonas;
            try
            {
                listaPersonas = new List<ClsPersona>(ClsListadoPersonasBL.getListadoPersonasCompletoBL());
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }

            if (listaPersonas == null || listaPersonas.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return ClsListadoPersonasBL.getListadoPersonasCompletoBL();
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public ClsPersona Get(int id)
        {
            return ClsListadoPersonasBL.getPersonaBL(id);
        }

        // POST api/<PersonasController>
        [HttpPost]
        public void Post([FromBody] ClsPersona oPersona)//[FromBody] 
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
