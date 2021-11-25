﻿using CRUD_Personas_BL.Listados;
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
        public IActionResult Index()
        {
            ClsListadoPersonasNombreDepartamentoVM personasDepart = null;
            try
            {
                personasDepart = new ClsListadoPersonasNombreDepartamentoVM();
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View(personasDepart.ListadoPersonasDepartamento);
        }
        #endregion

        #region ActionEdit
        public IActionResult Edit(int id)
        {
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(ClsListadoPersonasBL.getPersonaBL(id));
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View(oPersonaListadoDepartamentos);
        }

        [HttpPost]
        public IActionResult Edit(ClsPersona oPersona)
        {
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersona);
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersona);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View(oPersonaListadoDepartamentos);
        }
        #endregion

        #region ActionDelete
        public IActionResult Delete(int id)
        {
            ClsPersonaNombreDepartamento oPersonaNombreDepartamento;
            try
            {
                oPersonaNombreDepartamento = new ClsPersonaNombreDepartamento(ClsListadoPersonasBL.getPersonaBL(id));
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View(oPersonaNombreDepartamento);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            ClsListadoPersonasNombreDepartamentoVM personasDepartamento = null;
            try
            {
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonsaBL.eliminarPersonaBL(Id);
                personasDepartamento = new ClsListadoPersonasNombreDepartamentoVM();
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View("Index", personasDepartamento.ListadoPersonasDepartamento);
        }
        #endregion

        #region ActionInsert
        public IActionResult Insert()
        {
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM();
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View(oPersonaListadoDepartamentos);
        }

//        ObservableCollection<String> listadoNombresDepartamento;
//            try
//            {
//                listadoNombresDepartamento = ClsListadoDepartamentosBL.getListadoNombresDepartamentosBL();
//            }
//            catch (SqlException ex)
//            {
//                ViewBag.ErrorMsg = ex.Message;
//                return View("Error");
//}
//return View(listadoNombresDepartamento);

        [HttpPost]
        public IActionResult Insert(ClsPersonaNombreDepartamento oPersonaNombreDepartamento)
        {
            ClsPersonaListadoDepartamentosNombreDepartamentoVM oPersonaListadoDepartamentos = null;
            try
            {
                oPersonaNombreDepartamento.IdDepartamento = ClsListadoDepartamentosBL.getIdDepartamentoBL(oPersonaNombreDepartamento.NombreDepartamento);
                ViewBag.NumeroFilasAfectadas = ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersonaNombreDepartamento);
                oPersonaListadoDepartamentos = new ClsPersonaListadoDepartamentosNombreDepartamentoVM(oPersonaNombreDepartamento);
            }
            catch (SqlException ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            return View(oPersonaListadoDepartamentos);
        }
        #endregion
    }
}
