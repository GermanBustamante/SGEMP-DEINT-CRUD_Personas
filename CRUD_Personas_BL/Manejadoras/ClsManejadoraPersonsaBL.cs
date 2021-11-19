using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;

namespace CRUD_Personas_BL.Manejadoras
{
    public class ClsManejadoraPersonsaBL
    {
        public static void actualizarPersonaBL(ClsPersona oPersona)
        {
            ClsManejadoraPersonaDAL.actualizarPersonaDAL(oPersona);
        }
    }
}
