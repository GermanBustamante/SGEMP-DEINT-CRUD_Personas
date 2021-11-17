using CRUD_Personas_Core_UI.Models;
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
            //TODO MI CLASE CLSPERSONA DEBE TENER UN IDDEPARTAMENTO?
            ClsListadoPersonasVM personasDepart = new ClsListadoPersonasVM();
            return View(personasDepart.ListadoPersonasDepartamento);
        }
    }
}
