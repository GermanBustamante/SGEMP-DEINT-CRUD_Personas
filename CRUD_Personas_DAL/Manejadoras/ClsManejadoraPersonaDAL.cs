using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Utilidades;
using CRUD_Personas_Entidades;
using System;
using System.Data.SqlClient;

namespace CRUD_Personas_DAL.Manejadoras
{
    public class ClsManejadoraPersonaDAL : ClsUtilidadDMLDAL
    {
        #region constantes
        public const String INSTRUCCION_UPDATE_PERSONA = "UPDATE Personas SET ";
        public const String INSTRUCCION_UPDATE_PERSONA_CONDICION = " WHERE IDPersona =";
        public const String INSTRUCCION_DELETE_PERSONA_PK = "DELETE FROM Personas WHERE IDPersona=";
        #endregion

        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns></returns>
        public static int actualizarAñadirPersonaDAL(ClsPersona oPersona)
        {
            int resultado = 0;

            instanciarConexion();
            if (oPersona.Id != null)
            {
                resultado = ejecutarUpdatePersona(oPersona);
            }
            else
            {
                resultado = ejecutarInsertPersona(oPersona);
            }

            MiConexion.closeConnection();

            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int eliminarPersonaDAL(int id)
        {
            int resultado = 0;

            instanciarConexion();

            resultado = ejecutarSentenciaDMLCondicion(INSTRUCCION_DELETE_PERSONA_PK, id);

            MiConexion.closeConnection();

            return resultado;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentencia"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        private static int ejecutarUpdatePersona(ClsPersona oPersona)
        {
            aniadirParametrosPersonaMiComando(oPersona);
            //TODO CTE
            MiComando.CommandText = $"UPDATE Personas SET nombrePersona = @nombre, apellidosPersona = @apellidos, " +
                $"fechaNacimiento = @fechaNacimiento, telefono = @telefono, direccion = @direccion, foto = @foto, " +
                $"IDDepartamento = @idDepartamento WHERE IDPersona = @id";
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns></returns>
        private static int ejecutarInsertPersona(ClsPersona oPersona)
        {
            aniadirParametrosPersonaMiComando(oPersona);
            //TODO CTE
            MiComando.CommandText = "INSERT INTO PERSONAS VALUES (@nombre,@apellidos,@fechaNacimiento,@telefono,@direccion,@foto,@idDepartamento)";
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPersona"></param>
        private static void aniadirParametrosPersonaMiComando(ClsPersona oPersona)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = oPersona.Id;
            MiComando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = oPersona.Nombre;
            MiComando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar).Value = oPersona.Apellidos;
            MiComando.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = oPersona.FechaNacimiento;
            MiComando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = oPersona.Direccion;
            MiComando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = oPersona.Telefono;
            MiComando.Parameters.Add("@foto", System.Data.SqlDbType.VarChar).Value = oPersona.Foto;
            MiComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = oPersona.IdDepartamento;
        }
        #endregion
    }
}
