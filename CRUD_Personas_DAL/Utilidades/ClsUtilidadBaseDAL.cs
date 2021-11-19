using CRUD_Personas_DAL.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_Personas_DAL.Utilidades
{
    public abstract class ClsUtilidadBaseDAL
    {
        #region propiedades publicas
        public static ClsMyConnection MiConexion { get; set; }
        public static SqlCommand MiComando { get; set; }
        #endregion
        #region metodos publicos
        public static void instanciarConexion()
        {
            MiConexion = new ClsMyConnection();
            MiComando = new SqlCommand();
            MiConexion.getConnection();
        }
        #endregion

    }
}
