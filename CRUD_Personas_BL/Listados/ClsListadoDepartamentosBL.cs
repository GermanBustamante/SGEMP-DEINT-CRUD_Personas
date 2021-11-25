using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CRUD_Personas_BL.Listados
{
    public class ClsListadoDepartamentosBL
    {
        /// <summary>
        /// Devuelve una lista con los nombres de los departamentos de la tabla Departamentos
        /// </summary>
        /// <returns></returns>
        public  static ObservableCollection<String> getListadoNombresDepartamentosBL()
        {
            return ClsListadoDepartamentosDAL.getListadoNombresDepartamentosDAL();
        }

        /// <summary>
        /// Dado el id de un departamento, devuelve el nombre de este
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns></returns>
        public static String getNombreDepartamentoBL(int idDepartamento)
        {
            return ClsListadoDepartamentosDAL.getNombreDepartamentoDAL(idDepartamento);
        }

        /// <summary>
        /// Dado el nombre de un departamento, devuelve su Id
        /// </summary>
        /// <param name="nombreDepartamento"></param>
        /// <returns></returns>
        public static int getIdDepartamentoBL(string nombreDepartamento)
        {
            return ClsListadoDepartamentosDAL.getIdDepartamentoDAL(nombreDepartamento);
        }

       public static ObservableCollection<ClsDepartamento> getListadoDepartamentosBL()
        {
            return ClsListadoDepartamentosDAL.getListadoDepartamentosDAL();
        }
    }
}
