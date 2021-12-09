using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Utilidades;
using CRUD_Personas_Entidades;
using System;
using System.Data.SqlClient;

namespace CRUD_Personas_DAL.Manejadoras
{
    /// <summary>
    /// Contiene  todos los métodos y ctes necesarias para el manejo de personas de mi capa DAL
    /// </summary>
    public class ClsManejadoraPersonasDAL : ClsUtilidadDMLDAL
    {
        #region constantes
        public const String INSTRUCCION_DELETE_PERSONA_PK = "DELETE FROM Personas WHERE IDPersona=";
        public const String INSTRUCCION_UPDATE_PERSONA_PK = "UPDATE Personas SET nombrePersona = @nombre, apellidosPersona = @apellidos, " +
                "fechaNacimiento = @fechaNacimiento, telefono = @telefono, direccion = @direccion, foto = @foto, " +
                "IDDepartamento = @idDepartamento WHERE IDPersona = @idPersona";
        public const string INSTRUCCION_INSERT_PERSONA = "INSERT INTO Personas VALUES (@nombre,@apellidos,@fechaNacimiento,@telefono,@direccion,@foto,@idDepartamento)";
        #endregion

        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static int actualizarAñadirPersonaDAL(ClsPersona oPersona)<br/>
        /// <b>Comentarios:</b> Añade una persona o actualiza pasada por parámetro a la BD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b>Abre una conexión a la BD, y, dado una persona, si tiene un Id la actualiza en la BD, en caso contrario, la añade, al final, devuelve
        /// el numero de filas afectadas y cierra la conexión
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns> int representando el número de filas afectadas</returns>
        public static int actualizarAñadirPersonaDAL(ClsPersona oPersona)
        {
            instanciarConexion();
            aniadirParametrosPersonaMiComando(oPersona);
            int resultado = oPersona.Id == 0 ? ejecutarSentenciaDML(INSTRUCCION_INSERT_PERSONA) : ejecutarSentenciaDML(INSTRUCCION_UPDATE_PERSONA_PK);
            MiConexion.closeConnection();
            return resultado;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static int eliminarPersonaDAL(int idPersona)<br/>
        /// <b>Comentarios:</b> Elimina a una persona de la BD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Dado el id de una persona, abre una conexión, elimina a la persona con ese id, cierra y la conexión y devuelve el número de filas afectadas
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns> int representando el número de filas afectadas</returns>
        public static int eliminarPersonaDAL(int idPersona)
        {
            int resultado = 0;
            instanciarConexion();
            resultado = ejecutarSentenciaDMLCondicion(INSTRUCCION_DELETE_PERSONA_PK, idPersona);
            MiConexion.closeConnection();
            return resultado;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// <b>Prototipo:</b> private static void aniadirParametrosPersonaMiComando(ClsPersona oPersona)<br/>
        /// <b>Comentarios:</b> Añade a los paramétros a un SqlCommand<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Dado una persona, añade los parámetros de dicha persona al SqlCommand heredado MiComando, en caso de que pueda existir 
        /// algún valor nulo, lo controla
        /// </summary>
        /// <param name="oPersona"></param>
        private static void aniadirParametrosPersonaMiComando(ClsPersona oPersona)
        {
            MiComando.Parameters.Add("@idPersona", System.Data.SqlDbType.Int).Value = oPersona.Id;
            MiComando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = oPersona.Nombre;
            MiComando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar).Value = oPersona.Apellidos;
            MiComando.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = oPersona.FechaNacimiento;
            MiComando.Parameters.Add(!(oPersona.Direccion == null || oPersona.Direccion.Equals("")) ? new SqlParameter("@direccion", oPersona.Direccion) : new SqlParameter("@direccion", DBNull.Value));
            MiComando.Parameters.Add(!(oPersona.Telefono == null || oPersona.Telefono.Equals("")) ? new SqlParameter("@telefono",oPersona.Telefono) : new SqlParameter("@telefono", DBNull.Value));
            MiComando.Parameters.Add(!(oPersona.Foto == null || oPersona.Foto.Equals("")) ? new SqlParameter("@foto",oPersona.Foto) : new SqlParameter("@foto", DBNull.Value));
            MiComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = oPersona.IdDepartamento;
        }
        #endregion
    }
}          

