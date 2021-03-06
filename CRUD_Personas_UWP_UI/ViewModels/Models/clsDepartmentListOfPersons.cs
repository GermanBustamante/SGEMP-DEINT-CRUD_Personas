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
    /// <summary>
    /// Clase que usara el VM clsDepartmentsPageVM, hereda de ClsDepartamento<br/>
    /// <b>Propiedades:</b> ObservableCollection*ClsPersona* ListadoPersonas Consultable/Modificable
    /// </summary>
    public class clsDepartmentListOfPersons : ClsDepartamento
    {
        #region propiedades publicas        
        public ObservableCollection<ClsPersona> ListadoPersonas { get; set; }
        #endregion 
        #region constructores
        public clsDepartmentListOfPersons() { }

        public clsDepartmentListOfPersons(ClsDepartamento oDepartamento, ObservableCollection<ClsPersona> listadoPersonas) : base(oDepartamento.Id, oDepartamento.Nombre)
        {
            ListadoPersonas = listadoPersonas;
        }
        #endregion

    }
}
