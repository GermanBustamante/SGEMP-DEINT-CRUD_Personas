using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;

namespace CRUD_Personas_Core_UI.Models.ViewModels
{
    public class ClsPersonaListadoDepartamentosNombreDepartamentoVM : ClsPersonaNombreDepartamento
    {
        #region propiedades publicas
        public List<string> ListadoNombresDepartamentos { get; set; }
        #endregion
        #region constructores
        public ClsPersonaListadoDepartamentosNombreDepartamentoVM(ClsPersona oPersona)
                       : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {

            ListadoNombresDepartamentos = ClsListadoDepartamentosBL.getListadoNombresDepartamentosBL();
        }

        public ClsPersonaListadoDepartamentosNombreDepartamentoVM()
        {

            ListadoNombresDepartamentos = ClsListadoDepartamentosBL.getListadoNombresDepartamentosBL();
        }
        #endregion
    }
}
