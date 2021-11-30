using CRUD_Personas_DAL.Utilidades;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Personas_DAL.Manejadoras
{
    public class ClsManejadoraDepartamentoDAL : ClsUtilidadDMLDAL
    {
        #region constantes
        public const String INSTRUCCION_UPDATE_DEPARTAMENTO = "UPDATE Departamentos SET nombreDepartamento = @nombre WHERE IDDepartamento = @idDepartamento";
        public const String INSTRUCCION_INSERT_DEPARTAMENTO = "INSERT INTO Departametos VALUES (@nombreDepartamento)";
        #endregion

        #region metodos publicos
        public static int actualizarAniadirDepartamentoDAL(ClsDepartamento oDepartamento)
        {
            instanciarConexion();
            aniadirParametrosDepartamentoMiComando(oDepartamento);
            int resultado = oDepartamento.Id == 0 ? ejecutarSentenciaDML(INSTRUCCION_INSERT_DEPARTAMENTO) : ejecutarSentenciaDML(INSTRUCCION_UPDATE_DEPARTAMENTO);
            MiConexion.closeConnection();
            return resultado;
        }


        #endregion

        #region metodos privados
        private static void aniadirParametrosDepartamentoMiComando(ClsDepartamento oDepartamento)
        {
            MiComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = oDepartamento.Id;
            MiComando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = oDepartamento.Nombre;
        }
        #endregion
    }
}
