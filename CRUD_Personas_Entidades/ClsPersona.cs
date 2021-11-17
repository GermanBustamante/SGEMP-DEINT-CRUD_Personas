using System;

namespace CRUD_Personas_Entidades
{
    public class ClsPersona
    {
        #region atributos privados
        private string nombre;
        private string apellidos;
        public int IdDepartamento { get; set; }
        #endregion
        #region constructores
        //Constructor por defecto
        public ClsPersona()
        {
        }
        //Constructores por parametros
        public ClsPersona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellidos = apellido;
        }
        public ClsPersona(int id,string nombre, string apellido, DateTime fechaNacimiento, String direccion, String telefono, int idDepartamento)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellido;
            FechaNacimiento = fechaNacimiento;
            Direccion = direccion;
            Telefono = telefono;
            IdDepartamento = idDepartamento;
        }
        #endregion
        //GET +  SET
        #region propiedades publicas
        public int Id { get; set; }
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            { nombre = value; }
        }
        public string Apellidos
        {
            get
            { return apellidos; }
            set
            { apellidos = value; }
        }

        public DateTime FechaNacimiento { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        #endregion
    }
}
