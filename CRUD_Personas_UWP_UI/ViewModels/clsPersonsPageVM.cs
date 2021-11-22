using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsPersonsPageVM
    {
        //Mi vista necesita una lista de personas y la persona seleccionada(ClsPersonaNombreDepartamento), 
        //TUn
        public ObservableCollection<ClsPersona> ListadoPersonas { get; set; }
        public ClsPersona oPersonaSeleccionada { get; set; }        

        #region constructores
        public clsPersonsPageVM()
        {
            ListadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
        }
        #endregion
    }
}
