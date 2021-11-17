using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_Entidades;
using DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRUD_Personas_DAL.Listados
{
    public class ClsListadoPersonasDAL : ClsUtililidadSelectDAL
    {
        #region constantes
        public const String INSTRUCCION_SELECT_ALL_PERSONAS = "SELECT * FROM Personas";
        #endregion

        #region atributos
        //private static ClsMyConnection miConexion;
        //private static SqlCommand miComando;
        //private static SqlDataReader miLector;
        //private static List<ClsPersona> listadoPersonasRecogido;
        //private static ClsPersona oPersonaRecogida;
        #endregion

        #region propiedades publicas
        #endregion

        #region metodos publicos
        public static List<ClsPersona> getListaCompletaTablaPersonas()
        {
            List<ClsPersona> listadoPersonasRecogido = new List<ClsPersona>();
            MiConexion = new ClsMyConnection();
            MiComando = new SqlCommand();

            MiConexion.getConnection();

            MiLector = ejecutarSelectSinCondicion(INSTRUCCION_SELECT_ALL_PERSONAS);
            if (MiLector.HasRows)
            {
                listadoPersonasRecogido = RellenarListadoPersonas();
            }
            MiConexion.closeConnection();

            
            return listadoPersonasRecogido;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// 
        /// </summary>
        /// <param name="miLector"></param>
        /// <param name="oPersona"></param>
        private static List<ClsPersona> RellenarListadoPersonas()
        {
            List<ClsPersona> listadoPersonasRecogido = new List<ClsPersona>();
            ClsPersona oPersonaRecogida = null;
            while (MiLector.Read())
            {
                oPersonaRecogida = new ClsPersona();
                oPersonaRecogida.Id = (int)MiLector["IDPersona"];
                oPersonaRecogida.Nombre = (string)MiLector["nombrePersona"];
                oPersonaRecogida.Apellidos = (string)MiLector["apellidosPersona"];
                oPersonaRecogida.FechaNacimiento = (DateTime)MiLector["fechaNacimiento"];
                oPersonaRecogida.Telefono = (string)MiLector["telefono"];
                oPersonaRecogida.Direccion = (string)MiLector["direccion"];
                if (MiLector["IDDepartamento"] != System.DBNull.Value)
                {
                    oPersonaRecogida.IdDepartamento = (int)MiLector["IDDepartamento"];
                }
                listadoPersonasRecogido.Add(oPersonaRecogida);
            }
            return listadoPersonasRecogido;
        }

        //private static SqlDataReader ejecutarSelectSinCondicion(String instruccion)
        //{
        //    miComando.CommandText = instruccion;
        //    miComando.Connection = miConexion.Conexion;
        //    return miComando.ExecuteReader();
        //}
        #endregion
    }
}
