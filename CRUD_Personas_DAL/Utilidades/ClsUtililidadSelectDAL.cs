﻿using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Utilidades
{
    /// <summary>
    /// Esta clase contendrá todas las propiedades y métodos que usaremos en una clase que ejecute instrucciones SELECT
    /// </summary>
    public abstract class ClsUtililidadSelectDAL : ClsUtilidadBaseDAL
    {
        //NOTA: Dichos métodos sobre esta clase no controlan ninguna SqlException ya que lo lanzan para
        //que se encarguen el método que lo llama
        #region propiedades publicas
        public static SqlDataReader MiLector { get; set; }
        #endregion

        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static SqlDataReader ejecutarSelectCondicion(String instruccionSelect, int condicion)<br/>
        /// <b>Comentarios:</b> Ejecuta una instrucción Select con condición, normalmente esta será una PK<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Dado las propiedades heredadas, una instrucción select y una condición, ejecuta una instrucción Select con un parámetro condición,
        /// devolviendo el resultado correspondiente 
        /// </summary>
        /// <param name="instruccionSelect"></param>
        /// <param name="condicion"></param>
        /// <returns> SqlDataReader flujo de filas de solo avance resultante de la instrucción</returns>
        public static SqlDataReader ejecutarSelectCondicion(String instruccionSelect, int condicion)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = condicion;
            MiComando.Connection = MiConexion.Conexion;
            MiComando.CommandText = instruccionSelect + "@id";//TODO HACER CTE
            return MiComando.ExecuteReader();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static SqlDataReader ejecutarSelectCondicion(String instruccionSelect, String condicion)<br/>
        /// <b>Comentarios:</b> Ejecuta una instrucción Select con condición, normalmente esta será una PK<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Dado las propiedades heredadas, una instrucción select y una condición, ejecuta una instrucción Select con un parámetro condición,
        /// devolviendo el resultado correspondiente 
        /// </summary>
        /// <param name="instruccionSelect"></param>
        /// <param name="condicion"></param>
        /// <returns> SqlDataReader flujo de filas de solo avance resultante de la instrucción</returns>
        public static SqlDataReader ejecutarSelectCondicion(String instruccionSelect, String condicion)
        {
            MiComando.Parameters.Add("@id", System.Data.SqlDbType.VarChar).Value = condicion;
            MiComando.Connection = MiConexion.Conexion;
            MiComando.CommandText = instruccionSelect + "@id";//TODO HACER CTE
            return MiComando.ExecuteReader();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static SqlDataReader ejecutarSelect(String instruccionSelect)<br/>
        /// <b>Comentarios:</b> Ejecuta una instrucción Select y la devuelve<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Dado las propiedades heredadas y una instrucción select ejecuta dicha instrucción Select, luego,
        /// devuelve el resultado correspondiente
        /// </summary>
        /// <param name="instruccionSelect"></param>
        /// <returns> SqlDataReader flujo de filas de solo avance resultante de la instrucción</returns>
        public static SqlDataReader ejecutarSelect(String instruccionSelect)
        {
            MiComando.CommandText = instruccionSelect;
            MiComando.Connection = MiConexion.Conexion;
            return MiComando.ExecuteReader();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static void cerrarFlujos()<br/>
        /// <b>Comentarios:</b> Cierra los flujos de conexión tantos de esta clase como los heredados<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones: </b>Cierra el flujo de conexión tanto del objeto tipo MiConexion como el tipo MiLector 
        /// </summary>
        public static void cerrarFlujos()
        {
            MiConexion.closeConnection();
            MiLector.Close();
        }
        #endregion
    }
}
