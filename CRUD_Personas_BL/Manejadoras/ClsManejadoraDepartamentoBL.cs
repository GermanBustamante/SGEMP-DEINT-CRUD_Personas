using CRUD_Personas_DAL.Manejadoras;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Personas_BL.Manejadoras
{    /// <summary>
     /// Contiene las reglas de negocio que se aplicarán a los objetos tipo Departamento con los que se tratará en la capa DAL
     /// </summary>
    public class ClsManejadoraDepartamentoBL
    {

        /// <summary>
        /// <b>Prototipo:</b> public static int actualizarAniadirDepartamentoBL(ClsDepartamento oDepartamento)<br/>
        /// <b>Comentarios:</b> Actualiza o añade un departamento pasado por parámetro<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> ClsManejadoraDepartamentoDAL.actualizarAniadirDepartamentoDAL() pasandole por parámetro el parámetro actual,
        /// si hubiera reglas de negocio al tratar con este departamento, irán aquí
        /// pasada por parámetro
        /// </summary>
        /// <param name="oDepartamento"></param>
        /// <returns> int representando el número de filas afectadas</returns>
        public static int actualizarAniadirDepartamentoBL(ClsDepartamento oDepartamento)
        {
            return ClsManejadoraDepartamentoDAL.actualizarAniadirDepartamentoDAL(oDepartamento);
        }

        /// <summary>
        /// <b>Prototipo:</b> public static int eliminarDepartamentoBL(int idDepartamento)<br/>
        /// <b>Comentarios:</b> Elimina un departamento en la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Llama al método ClsManejadoraDepartamentoDAL.eliminarDepartamentoDAL y le pasa el id del departamento a eliminar,
        /// si hubiera reglas de negocio al tratar con este departamento, irán aquí
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns> int representando el número de filas afectadas</returns>
        public static int eliminarDepartamentoBL(int idDepartamento)
        {
            return ClsManejadoraDepartamentoDAL.eliminarDepartamentoDAL(idDepartamento);
        }
    }
}
