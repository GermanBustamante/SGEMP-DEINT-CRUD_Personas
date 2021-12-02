
using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Models.ViewModels
{
    /// <summary>
    /// Clase que hereda de ClsPersona y, además, contiene una propiedad que contiene el nombre del departamento de dicha
    /// persona, este VM lo usarán tanto las vistas Delete y Details como la clase 
    /// </summary>
    public class ClsPersonaNombreDepartamentoVM : ClsPersona
    {
        #region propiedades publicas
        public String NombreDepartamento { get; set; }
        #endregion
        #region constructores
        public ClsPersonaNombreDepartamentoVM()
        {
        }
        public ClsPersonaNombreDepartamentoVM(ClsPersona oPersona)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
                NombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(oPersona.IdDepartamento);
        }
        #endregion

    }
}
