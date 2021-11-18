
using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;

namespace CRUD_Personas_BL.Listados
{
    public class ClsListadoPersonasBL
    {
        public static List<ClsPersona> getListadoPersonasCompletoBL()
        {
            return ClsListadoPersonasDAL.getListaCompletaTablaPersonasDAL();
        }

        public static ClsPersona getPersonaDadoIdBL(int id)
        {
            return ClsListadoPersonasDAL.getPersonaDAL(id);
        }
    }
}
