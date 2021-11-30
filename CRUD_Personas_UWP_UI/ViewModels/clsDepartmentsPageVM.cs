using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using CRUD_Personas_UWP_UI.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsDepartmentsPageVM
    {
        #region atributos
        #endregion
       
        public clsDepartmentsPageVM()
        {
            ListadoDepartametos = new ObservableCollection<clsDepartmentListOfPersons>();
            ObservableCollection<ClsDepartamento> listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            foreach (ClsDepartamento item in listadoDepartamentos)
            {
                ListadoDepartametos.Add(new clsDepartmentListOfPersons(item));
            } 
        }
        #region propiedades publicas
        public ObservableCollection<clsDepartmentListOfPersons> ListadoDepartametos { get; set; }
        #endregion
        #region constructores

        #endregion
    }
}
