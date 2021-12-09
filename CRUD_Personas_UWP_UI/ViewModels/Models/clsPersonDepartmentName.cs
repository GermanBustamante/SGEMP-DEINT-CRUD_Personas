using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Personas_UWP_UI.ViewModels.Models
{
    public class clsPersonDepartmentName : ClsPersona
    {
        #region propiedades publicas
        public String NombreDepartamento { get; set; }
        #endregion
        #region constructores
        public clsPersonDepartmentName()
        {
        }

        public clsPersonDepartmentName(ClsPersona oPersona, String departmentName)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
            NombreDepartamento = departmentName;
        }
        #endregion

    }
  
}
