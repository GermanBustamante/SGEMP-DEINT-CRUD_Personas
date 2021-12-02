using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_Entidades;
using DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_Personas_DAL.Listados
{
    /// <summary>
    /// Contiene  todos los métodos y ctes necesarias para el leer y retornas departamentos o atributos de este de mi capa DAL
    /// </summary>
    public class ClsListadoDepartamentosDAL : ClsUtililidadSelectDAL
    {
        #region constantes
        public const String COLUMNA_NOMBRE_DEPARTAMENTO = "nombreDepartamento";
        public const String COLUMNA_IDDEPARTAMENTO = "IDDepartamento";
        public const String TABLA_DEPARTAMENTOS = "Departamentos";
        public const String INSTRUCCION_SELECT_NOMBRE_DEPARTAMENTO_PK = "SELECT " + COLUMNA_NOMBRE_DEPARTAMENTO + " FROM "+ TABLA_DEPARTAMENTOS + " WHERE " + COLUMNA_IDDEPARTAMENTO + "=";
        public const String INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS = "SELECT "+COLUMNA_NOMBRE_DEPARTAMENTO + " FROM " + TABLA_DEPARTAMENTOS;
        public const String INTRUCCION_SELECT_ID_DEPARTAMENTO_DADO_NOMBRE = "SELECT "+COLUMNA_IDDEPARTAMENTO+ " FROM " + TABLA_DEPARTAMENTOS + " WHERE " + COLUMNA_NOMBRE_DEPARTAMENTO + "=";
        public const String INSTRUCCION_SELECT_DEPARTAMENTOS = "SELECT * FROM " + TABLA_DEPARTAMENTOS;
        #endregion

        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static int getIdDepartamentoDAL(string nombreDepartamento)<br/>
        /// <b>Comentarios:</b> Devuelve el id de un departamento dado un nombre<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select dado el nombre de un departamento pasado por parámetro
        /// ,lee su id, cierra los flujos y lo retorna
        /// </summary>
        /// <param name="nombreDepartamento"></param>
        /// <returns> int idDepartamento representando el id del departamento leido</returns>
        public static int getIdDepartamentoDAL(string nombreDepartamento)
        {
            int idDepartamento = 0;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INTRUCCION_SELECT_ID_DEPARTAMENTO_DADO_NOMBRE, nombreDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                idDepartamento = (int)MiLector[COLUMNA_IDDEPARTAMENTO];
            }
            cerrarFlujos();
            return idDepartamento;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static String getNombreDepartamentoDAL(int idDepartamento)<br/>
        /// <b>Comentarios:</b> Devuelve el nombre de un departamento dado su id<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select dado el id de un departamento pasado por parámetro
        /// ,lee su nombre, cierra los flujos y lo retorna
        /// <param name="idDepartamento"></param>
        /// <returns> string nombreDepartamento representando el nombre del departamento leido o null si la instrucción no devuelve ninguna fila</returns>
        public static String getNombreDepartamentoDAL(int idDepartamento)
        {
            String nombreDepartamento = null;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INSTRUCCION_SELECT_NOMBRE_DEPARTAMENTO_PK, idDepartamento);
            if (MiLector.HasRows)
            {
                MiLector.Read();
                nombreDepartamento = (String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO];
            }
            cerrarFlujos();
            return nombreDepartamento;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection *ClsDepartamento* getListadoDepartamentosDAL()<br/>
        /// <b>Comentarios:</b> Devuelve un listado de departamentos de la tabla Departamentos de la BBDD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select para leer todas las filas de la tabla Departamentos
        /// ,los instancia, añade a una lista, cierra los flujos y lo retorna
        /// </summary>
        /// <returns> ObservableCollection*ClsDepartamento* representando todas los departamentos de la tabla o null si la instrucción no devuelve ninguna fila</returns>
        public static ObservableCollection<ClsDepartamento> getListadoDepartamentosDAL()
        {
            ObservableCollection<ClsDepartamento> listadoDepartamentos = null;
            instanciarConexion();
            MiLector = ejecutarSelect(INSTRUCCION_SELECT_DEPARTAMENTOS);
            if (MiLector.HasRows)
            {
                listadoDepartamentos = getListadoDepartamentosMiLector();
            }
            cerrarFlujos();
            return listadoDepartamentos;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*String* getListadoNombresDepartamentosDAL()<br/>
        /// <b>Comentarios:</b> Devuelve un listado de nombres de departamentos de la tabla Departamentos de la BD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select para leer todos las nombres de la tabla Departamentos
        /// , añade a una lista, cierra los flujos y lo retorna
        /// </summary>
        /// <returns> ObservableCollection*String* representando los nombres de los departamentos de la tabla o null si la instrucción no devuelve ninguna fila</returns>
        public static ObservableCollection<String> getListadoNombresDepartamentosDAL()
        {
            ObservableCollection<String> listadoNombresDepartamentos = null;
            instanciarConexion();
            MiLector = ejecutarSelect(INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS);
            if (MiLector.HasRows)
            {
                listadoNombresDepartamentos = getListadoNombresDepartamentosMiLector();
            }
            cerrarFlujos();

            return listadoNombresDepartamentos;
        }
        #endregion

        #region metodos privados

        /// <summary>
        /// <b>Prototipo:</b> private static ObservableCollection*ClsDepartamento* getListadoDepartamentosMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve un listado de departamentos de un lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Lee los departamentos de MiLector heredado mientras haya filas, los instancia, añade 
        /// a una lista y luego devuelve esta
        /// </summary>
        /// <returns> ObservableCollection*ClsDepartamento* representando los departamentos de la tabla</returns>
        private static ObservableCollection<ClsDepartamento> getListadoDepartamentosMiLector()
        {
            ObservableCollection<ClsDepartamento> listadoDepartamentos = new ObservableCollection<ClsDepartamento>();
            while (MiLector.Read())
            {
                listadoDepartamentos.Add(getDepartamentoMiLector());
            }
            return listadoDepartamentos;
        }

        /// <summary>
        /// <b>Prototipo:</b> private static ObservableCollection<string> getListadoNombresDepartamentosMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve un listado de nombres de departamentos de un lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Lee todos las nombres del lector heredado, los añade a una lista y la retorna
        /// </summary>
        /// <returns> ObservableCollection*String* representando los nombres de los departamentos de la tabla</returns>
        private static ObservableCollection<string> getListadoNombresDepartamentosMiLector()
        {
            ObservableCollection<String> listadoNombresDepartamentos = new ObservableCollection<String>();
            while (MiLector.Read())
            {
                listadoNombresDepartamentos.Add((String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO]);
            }
            return listadoNombresDepartamentos;
        }

        /// <summary>
        /// <b>Prototipo:</b> private static ClsDepartamento getDepartamentoMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve un departamento del lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Lee un objeto tipo Departamento de MiLector heredado, lo instancia y lo devuelve
        /// </summary>
        /// <returns> ClsDepartamento representando el departamento leido</returns>
        private static ClsDepartamento getDepartamentoMiLector()
        {
            return new ClsDepartamento((int)MiLector[COLUMNA_IDDEPARTAMENTO],(String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO]);
        }
        #endregion
    }



}
