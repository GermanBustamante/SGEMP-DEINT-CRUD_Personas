
using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Models
{
    //Si necesito una ampliacion de la clase Persona también podría hacer que herede
    public class ClsPersonaDepartamento : ClsPersona
    {
        public String NombreDepartamento { get; set; }

        //public ClsPersonaDepartamento(ClsPersona oPersona)
        //{
        //    this.OPersona = oPersona;
        //    NombreDepartamento = ClsListadoDepartamentosDAL.getNombreDepartamento(oPersona.IdDepartamento);
        //}

        public ClsPersonaDepartamento(int id, string nombre, string apellidos, DateTime fechaNacimiento, string direccion, string telefono, String foto,int idDepartamento)
                       : base(id, nombre, apellidos, fechaNacimiento, direccion, telefono, foto,idDepartamento)
        {
            NombreDepartamento = ClsListadoDepartamentosDAL.getNombreDepartamentoDAL(idDepartamento);
        }
    }
}
