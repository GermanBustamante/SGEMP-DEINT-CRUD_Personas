
using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;

namespace CRUD_Personas_BL.Listados
{
    public class ClsListadoPersonasBL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ClsPersona> getListadoPersonasCompletoBL()
        {
            return ClsListadoPersonasDAL.getListaCompletaTablaPersonasDAL();
        }

        /// <summary>
        /// Devuelve un objeto tipo Persona dado su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ClsPersona getPersonaBL(int id)
        {
            return ClsListadoPersonasDAL.getPersonaDAL(id);
        }
    }
}
