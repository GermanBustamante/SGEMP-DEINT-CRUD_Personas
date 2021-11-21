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
        /// Ejecuta una sentencia DML a secas
        /// </summary>
        /// <param name="sentencia"></param>
        /// <returns></returns>
        public static int ejecutarSentenciaDML(String sentencia)
        {
            MiComando.CommandText = sentencia;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta una sentencia DML en principio sobre solo una fila
        /// </summary>
        /// <param name="sentencia"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public static int ejecutarSentenciaDMLCondicion(String sentencia, int primaryKey)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = primaryKey;
            MiComando.CommandText = sentencia+"@id";
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }
        #endregion
    }
}
