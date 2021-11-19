using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;

namespace CRUD_Personas_BL.Manejadoras
{
    public class ClsManejadoraPersonsaBL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPersona"></param>
        public static int actualizarPersonaBL(ClsPersona oPersona)
        {
            return ClsManejadoraPersonaDAL.actualizarPersonaDAL(oPersona);
        }
    }
}
