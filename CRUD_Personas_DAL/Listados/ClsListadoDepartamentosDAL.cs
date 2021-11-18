using CRUD_Personas_DAL.Conexion;
using DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_Personas_DAL.Listados
{
    public class ClsListadoDepartamentosDAL : ClsUtililidadSelectDAL
    {
        #region constantes
        public const String INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS = "SELECT nombreDepartamento FROM Departamentos WHERE IdDepartamento=";
        #endregion

        #region atributos
        #endregion
        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns></returns>
        public static String getNombreDepartamentoDAL (int idDepartamento)
        {
            String nombreDepartamento = null;
            MiConexion = new ClsMyConnection();
            MiComando = new SqlCommand();

            MiConexion.getConnection();
            MiLector = ejecutarSelectDadoPK(INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS, idDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                nombreDepartamento = (String)MiLector["nombreDepartamento"];
            }

            return nombreDepartamento;
        }
        #endregion


        //private static SqlDataReader ejecutarSelectDadoPK(String instruccionSelect, int primaryKey)
        //{
        //    MiComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = primaryKey;
        //    MiComando.Connection = MiConexion.Conexion;
        //    MiComando.CommandText = INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS+"@id";//TODO HACER CTE
        //    return MiComando.ExecuteReader();
        //}

        private void instanciarConexionComando()
        {

        }
    }


    //public static List<String> getListaCompletaNombresDepartamento()
    //{
    //    miConexion = new ClsMyConnection();
    //    miConexion.getConnection();
    //    miLector = ejecutarComando("SELECT nombreDepartamento FROM Departamentos");
    //    if (miLector.HasRows)
    //    {
    //        while (miLector.Read())
    //        {
    //            listadoNombresDepartamento.Add((String)miLector["nombreDepartamento"]);
    //        }
    //    }
    //    miConexion.closeConnection();

    //    return listadoNombresDepartamento;
    //}
}
