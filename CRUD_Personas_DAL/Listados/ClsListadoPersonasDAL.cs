using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_Entidades;
using DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace CRUD_Personas_DAL.Listados
{
    public class ClsListadoPersonasDAL : ClsUtililidadSelectDAL
    {
        #region constantes
        public const String INSTRUCCION_SELECT_ALL_PERSONAS = "SELECT * FROM Personas";
        public const String INSTRUCCION_SELECT_PERSONA_DADO_ID = "SELECT * FROM Personas WHERE IDPersona=";
        #endregion
        //TODO PREGUNTAR SI EN LAS CLASES LISTADOS, ES INTERESANTE TENER UNA LISTA Y UN ATRIBUTO DE LA LISTA
        //DE LA CLASE A SIEMPRE A BUSCAR PARA NO TENER QUE DECLARARLA VARIAS VECES

        #region atributos
        #endregion

        #region propiedades publicas

        #endregion

        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ClsPersona> getListaCompletaTablaPersonasDAL()
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = new ObservableCollection<ClsPersona>();
            instanciarConexion();

            MiLector = ejecutarSelect(INSTRUCCION_SELECT_ALL_PERSONAS);
            if (MiLector.HasRows)
            {
                listadoPersonasRecogido = RellenarListadoPersonas();
            }

            cerrarFlujos();
            
            return listadoPersonasRecogido;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ClsPersona getPersonaDAL(int id)
        {
            ClsPersona oPersonaRecogida = null;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INSTRUCCION_SELECT_PERSONA_DADO_ID, id);

            if (MiLector.HasRows)
            {//TODO CTES COLUMNA_X_TABLA_Y
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
            cerrarFlujos();

            return oPersonaRecogida;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// 
        /// </summary>
        /// <param name="miLector"></param>
        /// <param name="oPersona"></param>
        private static ObservableCollection<ClsPersona> RellenarListadoPersonas()
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = new ObservableCollection<ClsPersona>();
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
