using CRUD_Personas_DAL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CRUD_Personas_BL.Listados
{
    /// <summary>
    /// Esta clase contiene los métodos que llamarán a la capa DAL para obtener datos sobre la tabla Departamentos de nuestra BBDD, aquí
    /// se aplicarán las reglas de negocio sobre dichos objetos si es que hay
    /// </summary>
    public class ClsListadoDepartamentosBL
    {
        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsDepartamento* getListadoDepartamentosBL()<br/>
        /// <b>Comentarios:</b> Devuelve una lista con los departamentos de la tabla Departamentos<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna un listado de todas los departamentos de la tabla Departamentos, llamando al método correspondiente
        /// de la capa DAL
        /// </summary>
        /// <returns> ObservableCollection*ClsDepartamento* representando la lista de los departamentos de la BD</returns>
        public static ObservableCollection<ClsDepartamento> getListadoDepartamentosBL()
        {
            return ClsListadoDepartamentosDAL.getListadoDepartamentosDAL();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*string* getListadoNombresDepartamentosBL()<br/>
        /// <b>Comentarios:</b> Devuelve una lista con los nombres de los departamentos de la tabla Departamentos<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna un listado de todas los departamentos de la tabla Departamentos, llamando al método correspondiente
        /// de la capa DAL
        /// </summary>
        /// <returns> ObservableCollection*string* representando la lista de nombres de los departamentos de la BD</returns>
        public static ObservableCollection<string> getListadoNombresDepartamentosBL()
        {
            return ClsListadoDepartamentosDAL.getListadoNombresDepartamentosDAL();
        }

        /// <summary>
        /// <b>Prototipo:</b> public static String getNombreDepartamentoBL(int idDepartamento)<br/>
        /// <b>Comentarios:</b> Devuelve un nombre de un departamento de la capa DAL<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Retorna un nombre de un departamento de la tabla Departametentos dado su id, llamando al método correspondiente
        /// de la capa DAL
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns>String representando el nombre del departamento recogido de la BD</returns>
        public static String getNombreDepartamentoBL(int idDepartamento)
        {
            return ClsListadoDepartamentosDAL.getNombreDepartamentoDAL(idDepartamento);
        }

        /// <summary>
        /// <b>Prototipo:</b> public static int getIdDepartamentoBL(string nombreDepartamento)<br/>
        /// <b>Comentarios:</b> Devuelve el id de un departamento de la capa DAL<br/>
        /// <b>Precondiciones:</b> Los nombres de la tabla Departamentos deben ser UNIQUE<br/>
        /// <b>Postcondiciones:</b> Retorna un id de un departamento de la tabla Departametentos dado su nombre, llamando al método correspondiente
        /// de la capa DAL
        /// </summary>
        /// <param name="nombreDepartamento"></param>
        /// <returns>int representando el id del departamento recogido de la BD</returns>
        public static int getIdDepartamentoBL(string nombreDepartamento)
        {
            return ClsListadoDepartamentosDAL.getIdDepartamentoDAL(nombreDepartamento);
        }


    }
}
