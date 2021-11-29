using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;

namespace CRUD_Personas_BL.Manejadoras
{
    /// <summary>
    /// Contiene las reglas de negocio que se aplicarán a los objetos retornados por la capa DAL
    /// </summary>
    public class ClsManejadoraPersonaBL
    {
        /// <summary>
        /// <b>Prototipo:</b> public static int actualizarAñadirPersonaBL(ClsPersona oPersona)<br/>
        /// <b>Comentarios:</b> Actualiza o añade una persona pasada por parámetro<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Llama al método ClsManejadoraPersonaDAL.actualizarAñadirPersonaDAL y le pasa la persona
        /// pasada por parámetro
        /// </summary>
        /// <param name="oPersona"></param>
        /// <returns> int representando el número de filas afecta</returns>
        public static int actualizarAñadirPersonaBL(ClsPersona oPersona)
        {
            return ClsManejadoraPersonaDAL.actualizarAñadirPersonaDAL(oPersona);
        }

        /// <summary>
        /// <b>Prototipo:</b> public static int eliminarPersonaBL(int idPersona)<br/>
        /// <b>Comentarios:</b> Elimina una persona en la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Llama al método ClsManejadoraPersonaDAL.eliminarPersonaDAL y le pasa el id de la persona
        /// a eliminar
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns> int representando el número de filas afectada</returns>
        public static int eliminarPersonaBL(int idPersona)
        {
            return ClsManejadoraPersonaDAL.eliminarPersonaDAL(idPersona);
        }
    }
}
