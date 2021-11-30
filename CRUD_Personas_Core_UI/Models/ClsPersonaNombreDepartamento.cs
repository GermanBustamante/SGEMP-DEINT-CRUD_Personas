
using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Models
{
    //Si necesito una ampliacion de la clase Persona también podría hacer que herede
    public class ClsPersonaNombreDepartamento : ClsPersona
    {
        #region propiedades publicas
        public String NombreDepartamento { get; set; }
        #endregion
        #region constructores
        public ClsPersonaNombreDepartamento()
        {
        }

        public ClsPersonaNombreDepartamento(ClsPersona oPersona)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
            NombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(oPersona.IdDepartamento);
        }

        public ClsPersonaNombreDepartamento(ClsPersona oPersona, String nombreDepartamento)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
            NombreDepartamento = nombreDepartamento;
        }
        #endregion

    }
}
