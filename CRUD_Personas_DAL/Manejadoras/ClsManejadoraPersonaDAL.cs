using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Utilidades;
using CRUD_Personas_Entidades;
using System;
using System.Data.SqlClient;

namespace CRUD_Personas_DAL.Manejadoras
{
    public class ClsManejadoraPersonaDAL : ClsUtilidadDMLDAL
    {
        public const String INSTRUCCION_UPDATE_PERSONA= "UPDATE Personas SET ";
        public const String INSTRUCCION_UPDATE_PERSONA_CONDICION = " WHERE IDPersona =";

        //TODO DUDA NOS INTERESA VALIDAR SI EL NUMERO DE FILAS ES 1 PARA COMPROBAR QUE SE HA EDITADO UNA PERSONA?
        //YA QUE SI DA 0 ES Q ALGO HA FALLADO PERO LA EXCEPCION YA SALTA DE POR SI
        public static int actualizarPersonaDAL(ClsPersona oPersona)
        {
            int resultado = 0;

            instanciarConexion();

            String sentencia = INSTRUCCION_UPDATE_PERSONA + "nombrePersona = "+oPersona.Nombre+", apellidosPersona = "+oPersona.Apellidos
                +", fechaNacimiento = "+oPersona.FechaNacimiento.Date.ToShortDateString()+",telefono = "
                +oPersona.Telefono+", direccion = "+oPersona.Direccion+", foto = "+oPersona.Foto+", IDDepartamento = "+oPersona.IdDepartamento
                +INSTRUCCION_UPDATE_PERSONA_CONDICION;

            resultado = ejecutarSentenciaDML(sentencia, oPersona.Id);

            MiConexion.closeConnection();

            return resultado;
        }
    }
}
