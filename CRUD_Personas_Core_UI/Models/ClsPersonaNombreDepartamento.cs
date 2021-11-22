﻿
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
        public ClsPersonaNombreDepartamento(int id, string nombre, string apellidos, DateTime fechaNacimiento, string direccion, string telefono, String foto, int idDepartamento)
                       : base(id, nombre, apellidos, fechaNacimiento, direccion, telefono, foto, idDepartamento)
        {
            NombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(idDepartamento);
        }

        public ClsPersonaNombreDepartamento(ClsPersona oPersona)
                      : base(oPersona.Id, oPersona.Nombre, oPersona.Apellidos, oPersona.FechaNacimiento, oPersona.Direccion, oPersona.Telefono, oPersona.Foto, oPersona.IdDepartamento)
        {
            NombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(oPersona.IdDepartamento);
        }
        #endregion

    }
}