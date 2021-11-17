using CRUD_Personas_DAL.Listados;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Personas_BL.Listados
{
    public class ClsListadoDepartamentosBL
    {
        public static String getNombreDepartamentoBL(int idDepartamento)
        {
            return ClsListadoDepartamentosDAL.getNombreDepartamento(idDepartamento);
        }
    }
}
