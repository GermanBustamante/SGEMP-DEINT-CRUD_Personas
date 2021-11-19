using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_Personas_DAL.Utilidades
{
    public abstract class ClsUtilidadDMLDAL : ClsUtilidadBaseDAL
    {
        #region propiedades publicas
        #endregion

        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentencia"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public static int ejecutarSentenciaDML(ClsPersona oPersona)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = oPersona.Id;
            MiComando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = oPersona.Nombre;
            MiComando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar).Value = oPersona.Apellidos;
            MiComando.Parameters.Add("@fechaNacimiento", System.Data.SqlDbType.Date).Value = oPersona.FechaNacimiento;
            MiComando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = oPersona.Direccion;
            MiComando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = oPersona.Telefono;
            MiComando.Parameters.Add("@foto", System.Data.SqlDbType.VarChar).Value = oPersona.Foto;
            MiComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = oPersona.IdDepartamento;
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
        /// <param name="sentencia"></param>
        /// <returns></returns>
        public static int ejecutarSentenciaDML(String sentencia)
        {
            MiComando.CommandText = sentencia;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }
        #endregion
    }
}
