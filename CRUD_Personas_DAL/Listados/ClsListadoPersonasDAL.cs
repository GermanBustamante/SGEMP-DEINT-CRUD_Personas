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
        public const String INSTRUCCION_SELECT_PERSONA_DADO_ID = "SELECT * FROM Personas WHERE IDPersona=";
        #endregion

        #region atributos
        #endregion

        #region propiedades publicas
        
        #endregion

        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ClsPersona> getListaCompletaTablaPersonasDAL()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ClsPersona getPersonaDAL(int id)
        {
            ClsPersona oPersonaRecogida = null;
            MiConexion = new ClsMyConnection();
            MiComando = new SqlCommand();

            MiConexion.getConnection();
            MiLector = ejecutarSelectDadoPK(INSTRUCCION_SELECT_PERSONA_DADO_ID, id);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                oPersonaRecogida = new ClsPersona();
                oPersonaRecogida.Id = (int)MiLector["IDPersona"];
                oPersonaRecogida.Nombre = (string)MiLector["nombrePersona"];
                oPersonaRecogida.Apellidos = (string)MiLector["apellidosPersona"];
                oPersonaRecogida.FechaNacimiento = (DateTime)MiLector["fechaNacimiento"];
                oPersonaRecogida.Telefono = (string)MiLector["telefono"];
                oPersonaRecogida.Direccion = (string)MiLector["direccion"];
                if (MiLector["foto"] != System.DBNull.Value)
                {
                    oPersonaRecogida.Foto = (String)MiLector["foto"];
                }
                if (MiLector["IDDepartamento"] != System.DBNull.Value)
                {
                    oPersonaRecogida.IdDepartamento = (int)MiLector["IDDepartamento"];
                }
            }

            return oPersonaRecogida;
        }

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
                if (MiLector["foto"] != System.DBNull.Value)
                {
                    oPersonaRecogida.Foto = (String)MiLector["foto"];
                }
                if (MiLector["IDDepartamento"] != System.DBNull.Value)
                {
                    oPersonaRecogida.IdDepartamento = (int)MiLector["IDDepartamento"];
                }
                listadoPersonasRecogido.Add(oPersonaRecogida);
            }
            return listadoPersonasRecogido;
        }
        #endregion
    }
}
