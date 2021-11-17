using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Models
{
    public class ClsListadoPersonasVM
    {
        public List<ClsPersonaDepartamento> ListadoPersonasDepartamento { get; set; }

        public ClsListadoPersonasVM()
        {
            ListadoPersonasDepartamento = rellenarListaPersonasDepartamento();
        }

        private List<ClsPersonaDepartamento> rellenarListaPersonasDepartamento()
        {
            List<ClsPersonaDepartamento> listaPersonasDepartamento = new List<ClsPersonaDepartamento>();
            List<ClsPersona> listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompleto();
            foreach(ClsPersona oPersonaRecogida in listadoPersonas)
            {
                listaPersonasDepartamento.Add(new ClsPersonaDepartamento(
                    oPersonaRecogida.Id, oPersonaRecogida.Nombre, oPersonaRecogida.Apellidos, oPersonaRecogida.FechaNacimiento, oPersonaRecogida.Direccion, oPersonaRecogida.Telefono, oPersonaRecogida.IdDepartamento));
            }
            return listaPersonasDepartamento;
        }
    }
}
