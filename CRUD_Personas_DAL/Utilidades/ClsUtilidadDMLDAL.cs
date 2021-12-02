using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_Personas_DAL.Utilidades
{
    /// <summary>
    /// Contiene los atributos y métodos que SIEMPRE usaremos en las clases que ejecute sentencias DML, Insert, Update, Delete ...
    /// </summary>
    public abstract class ClsUtilidadDMLDAL : ClsUtilidadBaseDAL
    {
        //NOTA: Dichos métodos sobre esta clase no controlan ninguna SqlException ya que lo lanzan para
        //que se encarguen el método que lo llama

        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static int ejecutarSentenciaDML(String sentenciaDML)<br/>
        /// <b>Comentarios:</b> Ejecuta una sentencia DML<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mediante las propiedades heredadas y una sentenciaDML, ejecuta dicha sentencia y devolviendo
        /// el numero de filas afectado
        /// </summary>
        /// <param name="sentenciaDML"></param>
        /// <returns> int representando el número de filas afectadas por dicha sentenciaDML</returns>
        public static int ejecutarSentenciaDML(String sentenciaDML)
        {
            MiComando.CommandText = sentenciaDML;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static int ejecutarSentenciaDMLCondicion(String sentenciaDML, int condicion)<br/>
        /// <b>Comentarios:</b> Ejecuta una sentenciaDML DML con una condición, siendo esta normalmente una PK<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mediante las propiedades heredadas y una sentenciaDML sql con una condición,
        /// añade dicho parámetro y ejecuta la sentenciaDML completa, al final, devuelve el numero de filas afectado
        /// </summary>
        /// <param name="sentenciaDML"></param>
        /// <param name="condicion"></param>
        /// <returns> int representando el número de filas afectadas por dicha sentenciaDML</returns>
        public static int ejecutarSentenciaDMLCondicion(String sentenciaDML, int condicion)
        {
            MiComando.Parameters.Add("@param", System.Data.SqlDbType.Int).Value = condicion;
            MiComando.CommandText = sentenciaDML+"@param";
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteNonQuery();
        }
        #endregion
    }
}
