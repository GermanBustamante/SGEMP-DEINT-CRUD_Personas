using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;

namespace CRUD_Personas_BL.Manejadoras
{
    public class ClsManejadoraPersonsaBL
    {
        //TODO MODURAILIZAR Y DEPENDER DE SU SU ID DE LA PERSONA ES NULO
        /// <summary>
        /// Actualiza una persona de la BD
        /// </summary>
        /// <param name="oPersona"></param>
        public static int actualizarAñadirPersonaBL(ClsPersona oPersona)
        {
            return ClsManejadoraPersonaDAL.actualizarAñadirPersonaDAL(oPersona);
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

        
    }
}
