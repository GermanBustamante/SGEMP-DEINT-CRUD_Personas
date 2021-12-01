
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
        //Constructor cuando solo será 1 vez y no importa que lo busque aquí o el que le llamó
        public ClsPersonaNombreDepartamento(ClsPersona oPersona)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
            NombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(oPersona.IdDepartamento);
        } 

        //Este constructor cuando usamos una lista de objetos de esta clase, para que no busque reiteradas
        //veces en la BD, sino que su recoge el que lo llama la lista departamentos y le devuelve el suyo
        public ClsPersonaNombreDepartamento(ClsPersona oPersona, String nombreDepartamento)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
            NombreDepartamento = nombreDepartamento;
        }
        #endregion

    }
}
