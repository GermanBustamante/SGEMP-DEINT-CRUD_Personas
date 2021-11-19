using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Core_UI.Models;
using CRUD_Personas_Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Controllers
{
    public class PersonasController : Controller
    {
        public IActionResult Index()
        {
            ClsListadoPersonasVM personasDepart = new ClsListadoPersonasVM();
            return View(personasDepart.ListadoPersonasDepartamento);
        }

        public IActionResult Edit(int id)
        {
            ClsPersona oPersona= ClsListadoPersonasBL.getPersonaDadoIdBL(id);
            return View(oPersona);
        }

        [HttpPost]
        public IActionResult Edit(ClsPersona oPersona)
        {
            ClsManejadoraPersonsaBL.actualizarPersonaBL(oPersona);
            return View(oPersona);
        }
    }
}
