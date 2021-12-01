using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Personas_BL.Manejadoras
{
    public class ClsManejadoraDepartamentoBL
    {
        public static int actualizarAniadirDepartamentoBL(ClsDepartamento oDepartamento)
        {
            return ClsManejadoraDepartamentoDAL.actualizarAniadirDepartamentoDAL(oDepartamento);
        }

        public static int eliminarDepartamentoBL(int id)
        {
            return ClsManejadoraDepartamentoDAL.eliminarDepartamentoDAL(id);
        }
    }
}
