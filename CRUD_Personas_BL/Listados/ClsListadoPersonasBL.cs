
using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CRUD_Personas_BL.Listados
{
    /// <summary>
    /// Esta clase contiene los métodos que llamarán a la capa DAL para obtener datos sobre la tabla Personas de nuestra BBDD, aquí
    /// se aplicarán las reglas de negocio sobre dichos objetos si es que hay
    /// </summary>
    public class ClsListadoPersonasBL
    {
        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasCompletoBL()<br/>
        /// <b>Comentarios:</b>Devuelve un listado de personas completo de la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna un listado de todas las personas de la tabla Personas, llamando al método correspondiente
        /// de la capa DAL, si existieran reglas de negocio sobre estas personas, irían aquí
        /// </summary>
        /// <returns> ObservableCollection*ClsPersona* representando el listado de personas de la BBDD</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasCompletoBL()
        {
            return ClsListadoPersonasDAL.getListadoPersonasCompletoDAL();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasIdNombreApellidosIdDepartamentoBL()<br/>
        /// <b>Comentarios:</b>Devuelve un listado de personas con solo los atributos instanciados id, nombre, apellidos y IdDepartamento de la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna un listado de todas las personas de la tabla Personas,llamando al método correspondiente
        /// de la capa DAL,estos objetos solo tendrán instanciado los atributos id, nombre, apellidos y IdDepartamento,si existieran reglas de negocio sobre estas personas, irían aquí
        /// </summary>
        /// <returns> ObservableCollection*ClsPersona* representando el listado de personas recogido de la BBDD</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasIdNombreApellidosIdDepartamentoBL()
        {
            return ClsListadoPersonasDAL.getListadoPersonasIdNombreApellidosIdDepartamentoDAL();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasDepartamentoBL(int idDepartamento)<br/>
        /// <b>Comentarios:</b>Devuelve un listado de personas completo de la capa DAL que pertenezcan a un departamento<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Dado el id del departamento que se quiere filtrar, retorna un listado de todas las personas de la tabla Personas que pertenezcan a dicho departamento,
        /// llamando al método correspondiente de la capa DAL, si existieran reglas de negocio sobre estas personas, irían aquí
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns> ObservableCollection*ClsPersona* representando el listado de personas recogido de la BBDD</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasDepartamentoBL(int idDepartamento)
        {
            return ClsListadoPersonasDAL.getListadoPersonasDepartamentoDAL(idDepartamento);
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ClsPersona getPersonaBL(int idPersona)<br/>
        /// <b>Comentarios:</b> Devuelve una persona de la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna una persona de la tabla Personas dado su id, llamando al método correspondiente
        /// de la capa DAL, si existieran reglas de negocio sobre estas personas, irían aquí
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns>ClsPersona representando la persona obtenida de la BBDD</returns>
        public static ClsPersona getPersonaBL(int idPersona)
        {
            return ClsListadoPersonasDAL.getPersonaDAL(idPersona);
        }
    }
}
