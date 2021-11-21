using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;

namespace CRUD_Personas_BL.Manejadoras
{
    public class ClsManejadoraPersonsaBL
    {
        /// <summary>
        /// Actualiza una persona de la BD
        /// </summary>
        /// <param name="oPersona"></param>
        public static int actualizarPersonaBL(ClsPersona oPersona)
        {
            return ClsManejadoraPersonaDAL.actualizarPersonaDAL(oPersona);
        }

        /// <summary>
        /// Elimina una persona de la BD
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int eliminarPersonaBL(int id)
        {
            return ClsManejadoraPersonaDAL.eliminarPersonaDAL(id);
        }

        /// <summary>
        /// Aniade una persona a la BD
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns></returns>
        public static int aniadirPersonaBL(ClsPersona oPersona)
        {
            return ClsManejadoraPersonaDAL.aniadirPersonaDAL(oPersona);
        }
    }
}
