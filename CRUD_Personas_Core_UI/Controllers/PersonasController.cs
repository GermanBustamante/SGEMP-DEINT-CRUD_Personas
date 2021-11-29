using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Core_UI.Models;
using CRUD_Personas_Core_UI.Models.ViewModels;
using CRUD_Personas_DAL.Listados;
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
        #region ActionList
        public IActionResult Index(/*int numeroFilas*/)
        {
            IActionResult actionResult = null;
            ClsListadoPersonasNombreDepartamentoVM personasDepart = null;
            try
            {
                personasDepart = new ClsListadoPersonasNombreDepartamentoVM();
                actionResult = View(personasDepart.ListadoPersonasDepartamento);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
            }
            return actionResult;
        }
        #endregion

        #region ActionEdit
        public IActionResult Edit(int id)
        {
            IActionResult actionResult = null;
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(ClsListadoPersonasBL.getPersonaBL(id));
                actionResult = View(oPersonaListadoDepartamentos);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
            }
            return actionResult;
        }

        [HttpPost]
        public IActionResult Edit(ClsPersona oPersona)
        {
            IActionResult actionResult = null;
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona);
                actionResult = View(oPersonaListadoDepartamentos);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
            }
            return actionResult;
        }
        #endregion

        #region ActionDelete
        public IActionResult Delete(int id)
        {
            IActionResult actionResult = null;
            ClsPersonaNombreDepartamento oPersonaNombreDepartamento;
            try
            {
                oPersonaNombreDepartamento = new ClsPersonaNombreDepartamento(ClsListadoPersonasBL.getPersonaBL(id));
                actionResult = View(oPersonaNombreDepartamento);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult =  View("Error");
            }
            return actionResult;
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            IActionResult actionResult;
            try
            {
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonaBL.eliminarPersonaBL(Id);
                actionResult = RedirectToAction("Index");//, new {numeroFilas = ViewBag.NumeroFilasAfectadas}
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
            }
            return actionResult;
        }
        #endregion

        #region ActionInsert
        public IActionResult Insert()
        {
            IActionResult actionResult =null;
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM();
                actionResult = View(oPersonaListadoDepartamentos);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult =  View("Error");
            }
            return actionResult;
        }

        [HttpPost]
        public IActionResult Insert(ClsPersona oPersona)
        {
            IActionResult actionResult =null;
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona);
                actionResult = View(oPersonaListadoDepartamentos);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult =  View("Error");
            }
            return actionResult;
        }
        #endregion
        #region ActionDetails
        public IActionResult Details(int id)
        {
            IActionResult actionResult = null;
            ClsPersonaNombreDepartamento oPersonaNombreDepartamento;
            try
            {
                oPersonaNombreDepartamento = new ClsPersonaNombreDepartamento(ClsListadoPersonasBL.getPersonaBL(id));
                actionResult = View(oPersonaNombreDepartamento);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
            }
            return actionResult;
        }
        #endregion
    }
}
