using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CRUD_Personas_Core_UI.Models.ViewModels
{
    /// <summary>
    /// Clase que hereda de ClsPersonaNombreDepartamento, que este a su vez hereda de ClsPersona.
    /// La usaremos como ViewModel en las vistas Insert, Edit y Delete.
    /// </summary>
    public class ClsPersonaListadoDepartamentosNombreDepartamentoVM : ClsPersonaNombreDepartamentoVM
    {
        #region propiedades publicas
        public ObservableCollection<ClsDepartamento> ListadoDepartamentos { get; set; }
        #endregion
        #region constructores
        public ClsPersonaListadoDepartamentosNombreDepartamentoVM()
        {
            ListadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
        }
        public ClsPersonaListadoDepartamentosNombreDepartamentoVM(ClsPersona oPersona)
                       : base(oPersona)
        {
            ListadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
        }
        #endregion
    }
}
