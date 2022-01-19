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
    public class DepartamentosController : ControllerBase
    {
        // GET: api/<DepartamentosController>
        [HttpGet]
        public IEnumerable<ClsDepartamento> Get()
        {
            List<ClsDepartamento> listaDepartamentos;
            try
            {
                listaDepartamentos = new List<ClsDepartamento>(ClsListadoDepartamentosBL.getListadoDepartamentosBL());
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }

            if (listaDepartamentos == null || listaDepartamentos.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return listaDepartamentos;
        }

        // GET api/<DepartamentosController>/5
        [HttpGet("{id}")]
        public ClsDepartamento Get(int id)
        {
            ClsDepartamento oDepartamento;
            String nombreDepartamento;
            try
            {
                nombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(id);
                oDepartamento = new ClsDepartamento(id, nombreDepartamento);//TODO getDepartamentoBL ENTERO
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }

            if (nombreDepartamento == null)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return oDepartamento;
        }

        // POST api/<DepartamentosController>
        [HttpPost]
        public void Post([FromBody] ClsDepartamento oDepartamento)
        {
            ClsManejadoraDepartamentoBL.actualizarAniadirDepartamentoBL(oDepartamento);
        }

        // PUT api/<DepartamentosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ClsDepartamento oDepartamento)
        {
            ClsManejadoraDepartamentoBL.actualizarAniadirDepartamentoBL(oDepartamento);
        }

        // DELETE api/<DepartamentosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ClsManejadoraDepartamentoBL.eliminarDepartamentoBL(id);
        }
    }
}
