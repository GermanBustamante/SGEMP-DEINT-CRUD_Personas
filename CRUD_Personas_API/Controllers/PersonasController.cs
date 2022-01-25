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

            return listaPersonas;
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public ClsPersona Get(int id)
        {
            ClsPersona oPersona;
            try
            {
                oPersona = ClsListadoPersonasBL.getPersonaBL(id);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }

            if (oPersona == null)
            {
                throw new HttpResponseException(HttpStatusCode.OK);
            }

            return oPersona;
        }

        // POST api/<PersonasController>
        [HttpPost]
        public IActionResult Post([FromBody] ClsPersona oPersona)//[FromBody] 
        {
            int numeroFilasAfectadas = 0;
            IActionResult resultado;
            try
            {
                numeroFilasAfectadas = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
            }
            catch (HttpResponseException e)
            {
                resultado = BadRequest();
            }
            if (numeroFilasAfectadas == 0)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok();
            }
            return resultado;
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ClsPersona oPersona)
        {
            int numeroFilasAfectadas = 0;
            IActionResult resultado;
            try
            {
                numeroFilasAfectadas = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
            }
            catch (HttpResponseException e)
            {
                resultado = BadRequest();
            }
            if (numeroFilasAfectadas == 0)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok();
            }
            return resultado;
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int numeroFilasAfectadas = 0;
            IActionResult resultado;
            try
            {
                numeroFilasAfectadas = ClsManejadoraPersonaBL.eliminarPersonaBL(id);
            }
            catch (HttpResponseException e)
            {
                resultado = BadRequest();
            }
            if (numeroFilasAfectadas == 0)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok();
            }
            return resultado;
        }
    }
}
