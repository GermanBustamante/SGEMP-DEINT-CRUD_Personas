
using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;

namespace CRUD_Personas_BL.Listados
{
    public class ClsListadoPersonasBL
    {
        public static List<ClsPersona> getListadoPersonasCompleto()
        {
            return ClsListadoPersonasDAL.getListaCompletaTablaPersonas();
        }
    }
}
