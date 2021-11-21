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
        public const String INSTRUCCION_SELECT_NOMBRE_DEPARTAMENTO_PK = "SELECT nombreDepartamento FROM Departamentos WHERE IdDepartamento=";
        public const String INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS = "SELECT nombreDepartamento FROM Departamentos";
        public const String COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS = "nombreDepartamento";
        public const String COLUMNA_IDDEPARTAMENTO_TABLA_DEPARTAMENTOS = "IDDepartamento";
        public const String INTRUCCION_SELECT_ID_DEPARTAMENTO_DADO_NOMBRE = "SELECT IDDepartamento FROM Departamentos WHERE "+COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS+"=";
        #endregion

        #region propiedades publicas
        #endregion
        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreDepartamento"></param>
        /// <returns></returns>
        public static int getIdDepartamentoDAL(string nombreDepartamento)
        {
            int idDepartamento = 0;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INTRUCCION_SELECT_ID_DEPARTAMENTO_DADO_NOMBRE, nombreDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                idDepartamento = (int)MiLector[COLUMNA_IDDEPARTAMENTO_TABLA_DEPARTAMENTOS];
            }
            return idDepartamento;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns></returns>
        public static String getNombreDepartamentoDAL (int idDepartamento)
        {
            String nombreDepartamento = null;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INSTRUCCION_SELECT_NOMBRE_DEPARTAMENTO_PK, idDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                nombreDepartamento = (String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS];
            }

            cerrarFlujos();

            return nombreDepartamento;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<String> getListadoNombresDepartamentosDAL()
        {
            List<String> listadoNombresDepartamentos = null;
            instanciarConexion();
            MiLector = ejecutarSelect(INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS);

            if (MiLector.HasRows)
            {
                listadoNombresDepartamentos = rellenarListadoNombresDepartamento();
            }

            cerrarFlujos();

            return listadoNombresDepartamentos;
        }
        #endregion
        #region metodos privados
        private static List<string> rellenarListadoNombresDepartamento()
        {
            List<String> listadoNombresDepartamentos = new List<String>();
            while (MiLector.Read())
            {
                listadoNombresDepartamentos.Add((String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS]);
            }
            return listadoNombresDepartamentos;
        }
        #endregion
    }



}
