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

        public IActionResult Index(String resultado)
        {
            IActionResult actionResult = null;
            ClsListadoPersonasNombreApellidosNombreDepartamentoVM personasDepart = null;
            try
            {
                //Este constructor por defecto carga la lista de personas con nombre departamento
                personasDepart = new ClsListadoPersonasNombreApellidosNombreDepartamentoVM();
                ViewBag.Resultado = resultado;
                actionResult = View(personasDepart.ListadoPersonasNombreApellidosNombreDepartamento);
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
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentosNombreDepartamento = null;
            try
            {
                oPersonaListadoDepartamentosNombreDepartamento = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(ClsListadoPersonasBL.getPersonaBL(id));
                actionResult = View(oPersonaListadoDepartamentosNombreDepartamento);
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
                if (!ModelState.IsValid)
                {

                    actionResult = View("Edit", new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona));
                }
                else
                {
                    ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
                    oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona);
                    actionResult = View(oPersonaListadoDepartamentos);
                }
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
            ClsPersonaNombreDepartamentoVM oPersonaNombreDepartamento;
            try
            {
                oPersonaNombreDepartamento = new ClsPersonaNombreDepartamentoVM(ClsListadoPersonasBL.getPersonaBL(id));
                actionResult = View(oPersonaNombreDepartamento);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
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
                int numeroFilas = ClsManejadoraPersonaBL.eliminarPersonaBL(Id);
                String resultado = numeroFilas != 0 ? "Se ha borrado correctamente" : "No se ha borrado correctamente";
                actionResult = RedirectToAction("Index", new { resultado = resultado });
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
            IActionResult actionResult = null;
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM();
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
        public IActionResult Insert(ClsPersona oPersona)
        {
            IActionResult actionResult = null;
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    actionResult = View("Insert", new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona));
                }
                else
                {
                    ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersona);
                    oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona);
                    actionResult = View(oPersonaListadoDepartamentos);
                }
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                actionResult = View("Error");
            }
            return actionResult;
        }
        #endregion

        #region ActionDetails
        public IActionResult Details(int id)
        {
            IActionResult actionResult = null;
            ClsPersonaNombreDepartamentoVM oPersonaNombreDepartamento;
            try
            {
                oPersonaNombreDepartamento = new ClsPersonaNombreDepartamentoVM(ClsListadoPersonasBL.getPersonaBL(id));
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
