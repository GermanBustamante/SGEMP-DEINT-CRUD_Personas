using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CRUD_Personas_Core_UI.Models.ViewModels
{
    public class ClsPersonaListadoDepartamentosNombreDepartamentoVM : ClsPersonaNombreDepartamento
    {
        #region propiedades publicas
        public ObservableCollection<ClsDepartamento> ListadoDepartamentos { get; set; }
        #endregion
        #region constructores
        public ClsPersonaListadoDepartamentosNombreDepartamentoVM(ClsPersona oPersona)
                       : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {

            ListadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
        }

        public ClsPersonaListadoDepartamentosNombreDepartamentoVM()
        {

            ListadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
        }
        #endregion
    }
}
