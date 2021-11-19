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
        public const String COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS = "nombreDepartamento";
        #endregion

        #region atributos
        #endregion
        #region propiedades publicas
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
            instanciarConexion();
            MiLector = ejecutarSelectDadoPK(INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS, idDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                nombreDepartamento = (String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS];
            }

            cerrarFlujos();

            return nombreDepartamento;
        }
        #endregion
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
