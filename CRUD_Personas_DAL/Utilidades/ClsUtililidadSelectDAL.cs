using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Utilidades
{   
    //Esta clase contendrá todas las propiedades y métodos que usaremos en una clase que ejecute instrucciones SELECT
    public abstract class ClsUtililidadSelectDAL : ClsUtilidadBaseDAL
    {
        #region propiedades publicas
        public static SqlDataReader MiLector { get; set; }
        #endregion

        #region metodos publicos
        public static SqlDataReader ejecutarSelectDadoPK(String instruccionSelect, int primaryKey)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = primaryKey;
            MiComando.Connection = MiConexion.Conexion;
            MiComando.CommandText = instruccionSelect + "@id";//TODO HACER CTE
            return MiComando.ExecuteReader();
        }

        public static SqlDataReader ejecutarSelectSinCondicion(String instruccion)
        {
            MiComando.CommandText = instruccion;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteReader();
        }

        public static void instanciarConexion()
        {
            MiConexion = new ClsMyConnection();
            MiComando = new SqlCommand();
            MiConexion.getConnection();
        }

        public static void cerrarFlujos()
        {
            MiConexion.closeConnection();
            MiLector.Close();
        }
        #endregion





    }
}
