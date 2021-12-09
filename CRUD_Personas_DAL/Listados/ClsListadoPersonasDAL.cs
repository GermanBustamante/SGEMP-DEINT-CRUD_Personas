using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_Entidades;
using DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace CRUD_Personas_DAL.Listados
{
    /// <summary>
    /// Contiene  todos los métodos y ctes necesarias para el leer y retornas personas o atributos de esta de mi capa DAL
    /// </summary>
    public class ClsListadoPersonasDAL : ClsUtililidadSelectDAL
    {
        #region constantes
        public const String INSTRUCCION_SELECT_ALL_PERSONAS = "SELECT * FROM "+ TABLA_PERSONAS;
        public const String INSTRUCCION_SELECT_PERSONA_CONDICION_ID = "SELECT * FROM " + TABLA_PERSONAS +" WHERE "+ COLUMNA_ID +"=";
        public const String INSTRUCCION_SELECT_PERSONA_CONDICION_IDDEPARTAMENTO = "SELECT * FROM " + TABLA_PERSONAS + " WHERE " + COLUMNA_ID_DEPARTAMENTO + "=";
        public const String INSTRUCCION_SELECT_ALL_PERSONAS_ID_NOMBRE_APELLIDOS_IDDEPARTAMENTO = "SELECT +"+COLUMNA_ID+","+COLUMNA_NOMBRE+","+ COLUMNA_APELLIDOS+","+COLUMNA_ID_DEPARTAMENTO+" FROM "+TABLA_PERSONAS;
        public const String TABLA_PERSONAS = "Personas";
        public const String COLUMNA_ID = "IDPersona";
        public const String COLUMNA_NOMBRE = "nombrePersona";
        public const String COLUMNA_APELLIDOS = "apellidosPersona";
        public const String COLUMNA_FECHA_NACIMIENTO = "fechaNacimiento";
        public const String COLUMNA_DIRECCION = "direccion";
        public const String COLUMNA_FOTO = "foto";
        public const String COLUMNA_ID_DEPARTAMENTO = "IDDepartamento";
        public const String COLUMNA_TELEFONO = "telefono";
        #endregion

        #region metodos publicos
        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasCompletoDAL()<br/>
        /// <b>Comentarios:</b> Devuelve una lista de todas las personas de la BD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select, la lee, reconstruye las personas leidas
        /// y las añade a una lista, cierra los flujos y retorna dicha lista
        /// </summary>
        /// <returns>ObservableCollection*ClsPersona* siendo este el listado de personas de toda la tabla Personas de la BD</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasCompletoDAL()
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = null;
            instanciarConexion();
            MiLector = ejecutarSelect(INSTRUCCION_SELECT_ALL_PERSONAS);
            if (MiLector.HasRows)
            {
                listadoPersonasRecogido = getListadoPersonasMiLector();
            }
            cerrarFlujos();
            return listadoPersonasRecogido;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasIdNombreApellidosIdDepartamentoDAL()<br/>
        /// <b>Comentarios:</b> Devuelve una lista de todas las personas de la BD, pero solo con los atributos id,nombre, apellidos y idDepartamento seteados<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select, la lee, reconstruye las personas leidas
        /// y las añade a una lista, cierra los flujos y retorna dicha lista. Estas personas solo tendrán seteadas los atributos id, nombre, apellidos y idDepartamento
        /// </summary>
        /// <returns>ObservableCollection*ClsPersona* siendo este el listado de personas de toda la tabla Personas de la BD</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasIdNombreApellidosIdDepartamentoDAL()
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = new ObservableCollection<ClsPersona>();
            instanciarConexion();

            MiLector = ejecutarSelect(INSTRUCCION_SELECT_ALL_PERSONAS_ID_NOMBRE_APELLIDOS_IDDEPARTAMENTO);
            if (MiLector.HasRows)
            {
                listadoPersonasRecogido = getListadoPersonasIdNombreApellidosIdDepartamentoMiLector();
            }
            cerrarFlujos();
            return listadoPersonasRecogido;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ObservableCollection*ClsPersona* getListadoPersonasDepartamentoDAL(int idDepartamento)<br/>
        /// <b>Comentarios:</b> Devuelve una lista de todas las personas de la BD que pertenecen a un departamento<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select, la lee, reconstruye las personas leidas
        /// que pertezcan al departamento cuyo id es igual al pasado por parámetro y las añade a una lista, cierra los flujos y retorna dicha lista
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns>ObservableCollection*ClsPersona* siendo este el listado de personas de toda la tabla Personas que pertenezcan al departamento</returns>
        public static ObservableCollection<ClsPersona> getListadoPersonasDepartamentoDAL(int idDepartamento)
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = new ObservableCollection<ClsPersona>();
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INSTRUCCION_SELECT_PERSONA_CONDICION_IDDEPARTAMENTO, idDepartamento);
            if (MiLector.HasRows)
            {
                listadoPersonasRecogido = getListadoPersonasMiLector();
            }
            cerrarFlujos();
            return listadoPersonasRecogido;
        }

        /// <summary>
        /// <b>Prototipo:</b> public static ClsPersona getPersonaDAL(int idPersona)<br/>
        /// <b>Comentarios:</b> Devuelve una persona de la BDDD<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Abre una conexión a la BD, ejecuta una instrucción Select sobre el Id de una persona,
        /// la reconstruye, cierra los flujos y retorna dicho objeto persona
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns> ClsPersona representando la persona recogida de la BD o null si la instrucción no devuelve ninguna fila</returns>
        public static ClsPersona getPersonaDAL(int idPersona)
        {
            ClsPersona oPersonaRecogida = null;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INSTRUCCION_SELECT_PERSONA_CONDICION_ID, idPersona);
            if (MiLector.HasRows)
            {
                MiLector.Read();
                oPersonaRecogida = getPersonaMiLector();
            }
            cerrarFlujos();
            return oPersonaRecogida;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// <b>Prototipo:</b> private static ObservableCollection*ClsPersona* getListadoPersonasMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve un listado de personas de un lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mientras el lector heredado contenga filas, lee objetos tipo Persona del lector y las añade a una lista para luego retornarla
        /// </summary>
        /// <returns> ObservableCollection*ClsPersona* siendo esta el listado de personas leido</returns>
        private static ObservableCollection<ClsPersona> getListadoPersonasMiLector()
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = new ObservableCollection<ClsPersona>();
            while (MiLector.Read())
            {
                listadoPersonasRecogido.Add(getPersonaMiLector());
            }
            return listadoPersonasRecogido;
        }

        /// <summary>
        /// <b>Prototipo:</b> private static ObservableCollection<ClsPersona> getListadoPersonasIdNombreApellidosIdDepartamentoMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve un listado de personas de un lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mientras el lector heredado contenga filas, lee objetos tipo Persona del lector y las añade a una lista para luego retornarla,
        /// estos objetos tipo ClsPersona de la lista solo tendrán setados los atributos id, nombre, apellidos y idDepartamento
        /// </summary>
        /// <returns> ObservableCollection*ClsPersona* siendo esta el listado de personas leido</returns>
        private static ObservableCollection<ClsPersona> getListadoPersonasIdNombreApellidosIdDepartamentoMiLector()
        {
            ObservableCollection<ClsPersona> listadoPersonasRecogido = new ObservableCollection<ClsPersona>();
            while (MiLector.Read())
            {
                listadoPersonasRecogido.Add(getPersonaIdNombreApellidosIdDepartamentoMiLector());
            }
            return listadoPersonasRecogido;
        }

        /// <summary>
        /// <b>Prototipo:</b> private static ClsPersona getPersonaMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve una persona de un lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> El lector lee todos los atributos de un objeto tipo Persona y los instancia en los atributos
        /// de dicho objeto para luego retornarla
        /// </summary>
        /// <returns> ClsPersona objeto persona leido</returns>
        private static ClsPersona getPersonaMiLector()
        {
            ClsPersona oPersonaRecogida = new ClsPersona();
            oPersonaRecogida.Id = (int)MiLector[COLUMNA_ID];

            oPersonaRecogida.Nombre = (string)MiLector[COLUMNA_NOMBRE];
            oPersonaRecogida.Apellidos = (string)MiLector[COLUMNA_APELLIDOS];
            oPersonaRecogida.FechaNacimiento = (DateTime)MiLector[COLUMNA_FECHA_NACIMIENTO];
            if (MiLector[COLUMNA_TELEFONO]!= DBNull.Value)
            {
                oPersonaRecogida.Telefono = (string)(MiLector[COLUMNA_TELEFONO]);

            }
            if (MiLector[COLUMNA_DIRECCION] != DBNull.Value)
            {
                oPersonaRecogida.Direccion = (string)MiLector[COLUMNA_DIRECCION];
            }
            if (MiLector[COLUMNA_FOTO] != DBNull.Value)
            {
                oPersonaRecogida.Foto = (string)MiLector[COLUMNA_FOTO];
            }
            oPersonaRecogida.IdDepartamento = (int)MiLector[COLUMNA_ID_DEPARTAMENTO];
            return oPersonaRecogida;
        }

        /// <summary>
        /// <b>Prototipo:</b> private static ClsPersona getPersonaIdNombreApellidosIdDepartamentoMiLector()<br/>
        /// <b>Comentarios:</b> Devuelve una persona de un lector<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> El lector lee los atributos id, nombre, apellidos y idDepartamento de un objeto tipo Persona y los instancia en los atributos
        /// de dicho objeto para luego retornarla
        /// </summary>
        /// <returns> ClsPersona objeto persona leido</returns>
        private static ClsPersona getPersonaIdNombreApellidosIdDepartamentoMiLector()
        {
            ClsPersona oPersonaRecogida = new ClsPersona();
            oPersonaRecogida.Id = (int)MiLector[COLUMNA_ID];
            oPersonaRecogida.Nombre = (string)MiLector[COLUMNA_NOMBRE];
            oPersonaRecogida.Apellidos = (string)MiLector[COLUMNA_APELLIDOS];
            oPersonaRecogida.IdDepartamento = (int)MiLector[COLUMNA_ID_DEPARTAMENTO];
            return oPersonaRecogida;
        }
        #endregion
    }
}
