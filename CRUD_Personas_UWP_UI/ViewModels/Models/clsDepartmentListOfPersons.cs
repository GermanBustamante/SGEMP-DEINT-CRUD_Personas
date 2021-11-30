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
        #endregion
        #region propiedades publicas
        public ObservableCollection<ClsPersona> ListadoPersonasDepartamento{ get; set; }
        #endregion
        #region constructores
        public clsDepartmentListOfPersons() { }
        public clsDepartmentListOfPersons(ClsDepartamento oDepartamento) : base(oDepartamento.Id, oDepartamento.Nombre)
        {
            ListadoPersonasDepartamento = ClsListadoPersonasBL.getListadoPersonasDepartamentoBL(oDepartamento.Id);
        }
        #endregion

    }
}
