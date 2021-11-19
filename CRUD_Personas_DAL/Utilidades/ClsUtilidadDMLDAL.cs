using CRUD_Personas_DAL.Conexion;
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
        public static int ejecutarSentenciaDML(String sentencia, int primaryKey)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = primaryKey;
            MiComando.CommandText = sentencia+"@id";
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        public static int ejecutarSentenciaDML(String sentencia)
        {
            MiComando.CommandText = sentencia;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }
        #endregion
    }
}
