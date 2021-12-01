using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Personas_UWP_UI.ViewModels.Models
{
    public class clsDepartmentListOfPersons : ClsDepartamento
    {
        #region atributos
        public ObservableCollection<ClsPersona> ListadoPersonas { get; set; }
        #endregion
        #region propiedades publicas
        #endregion 
        #region constructores
        public clsDepartmentListOfPersons() { }
        public clsDepartmentListOfPersons(ClsDepartamento oDepartamento) : base(oDepartamento.Id, oDepartamento.Nombre)
        {
            ListadoPersonas = ClsListadoPersonasBL.getListadoPersonasDepartamentoBL(oDepartamento.Id);
        }

        public clsDepartmentListOfPersons(ClsDepartamento oDepartamento, ObservableCollection<ClsPersona> listadoPersonas) : base(oDepartamento.Id, oDepartamento.Nombre)
        {
            ListadoPersonas = listadoPersonas;
        }
        #endregion

    }
}
