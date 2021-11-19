using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Core_UI.Models;
using CRUD_Personas_Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Controllers
{
    public class PersonasController : Controller
    {
        public IActionResult Index()
        {
            ClsListadoPersonasVM personasDepart = null;
            try
            {
                personasDepart = new ClsListadoPersonasVM();
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error", ex);
            }
            return View(personasDepart.ListadoPersonasDepartamento);
        }

        public IActionResult Edit(int id)
        {
            ClsPersona oPersona = null;
            try
            {
                oPersona = ClsListadoPersonasBL.getPersonaDadoIdBL(id);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error", ex);
            }

            return View(oPersona);
        }

        //TODO VIEWMODEL CON DROP DESPLEGABLE DE LOS NOMBRES DE LAS LISTAS Y QUE ESTÉ POR DEFECTO EL ID
        [HttpPost]
        public IActionResult Edit(ClsPersona oPersona)
        {
            try
            {
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonsaBL.actualizarPersonaBL(oPersona);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error", ex);
            }
            return View(oPersona);
        }
    }
}
