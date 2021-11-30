
using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CRUD_Personas_BL.Listados
{
    public class ClsListadoPersonasBL
    {
        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasCompletoBL()<br/>
        /// <b>Comentarios:</b>Devuelve un listado de personas completo de la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna un listado de todas las personas de la tabla Personas, llamando al método correspondiente
        /// de la capa DAL
        /// </summary>
        /// <returns> ObservableCollection*ClsPersona* representando el listado de personas de la BD</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasCompletoBL()
        {
            return ClsListadoPersonasDAL.getListadoPersonasCompletoDAL();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ClsPersona getPersonaBL(int idPersona)<br/>
        /// <b>Comentarios:</b> Devuelve una persona de la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna una persona de la tabla Personas dado su id, llamando al método correspondiente
        /// de la capa DAL
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns>ClsPersona representando la persona obtenida de la BD</returns>
        public static ClsPersona getPersonaBL(int idPersona)
        {
            return ClsListadoPersonasDAL.getPersonaDAL(idPersona);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns></returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasDepartamentoBL(int idDepartamento)
        {
            return ClsListadoPersonasDAL.getListadoPersonasDepartamentoDAL(idDepartamento);
        }
    }
}
