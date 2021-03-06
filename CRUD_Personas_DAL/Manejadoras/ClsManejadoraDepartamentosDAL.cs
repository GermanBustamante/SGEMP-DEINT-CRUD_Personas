using CRUD_Personas_DAL.Utilidades;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Personas_DAL.Manejadoras
{
    public class ClsManejadoraDepartamentosDAL : ClsUtilidadDMLDAL
    {
        #region constantes
        public const String INSTRUCCION_UPDATE_DEPARTAMENTO = "UPDATE Departamentos SET nombreDepartamento = @nombreDepartamento WHERE IDDepartamento = @idDepartamento";
        public const String INSTRUCCION_INSERT_DEPARTAMENTO = "INSERT INTO Departamentos  VALUES (@nombreDepartamento)";
        public const String SP_ELIMINAR_DEPARTAMENTO = "SPEliminarDepartamento";
        public const String COLUMNA_ID_DEPARTAMENTO_TABLA_DEPARTAMENTOS = "IDDepartamento";
        public const String COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS = "nombreDepartamento";
        #endregion

        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDepartamento"></param>
        /// <returns></returns>
        public static int actualizarAniadirDepartamentoDAL(ClsDepartamento oDepartamento)
        {
            instanciarConexion();
            aniadirParametrosDepartamentoMiComando(oDepartamento);
            int resultado = oDepartamento.Id == 0 ? ejecutarSentenciaDML(INSTRUCCION_INSERT_DEPARTAMENTO) : ejecutarSentenciaDML(INSTRUCCION_UPDATE_DEPARTAMENTO);
            MiConexion.closeConnection();
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns></returns>
        public static int eliminarDepartamentoDAL(int idDepartamento)
        {
            int resultado = 0;
            instanciarConexion();
            MiComando.Parameters.Add("@IdDepartamento", System.Data.SqlDbType.SmallInt).Value = idDepartamento;
            resultado = ejecutarProcedimientoAlmacenado(SP_ELIMINAR_DEPARTAMENTO);
            MiConexion.closeConnection();
            return resultado;
            

        }
        #endregion

        #region metodos privados
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oDepartamento"></param>
        private static void aniadirParametrosDepartamentoMiComando(ClsDepartamento oDepartamento)
        {
            MiComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = oDepartamento.Id;
            MiComando.Parameters.Add("@nombreDepartamento", System.Data.SqlDbType.VarChar).Value = oDepartamento.Nombre;
        }
        #endregion
    }
}
