using System;

namespace CRUD_Personas_Entidades
{
    public class ClsPersona
    {
        #region atributos privados
        private string nombre;
        private string apellidos;
        #endregion
        #region constructores
        //Constructor por defecto
        public ClsPersona()
        {
        }
        //Constructores por parametros
        public ClsPersona(int id,string nombre, string apellido, DateTime fechaNacimiento, String direccion, String telefono,String foto ,int idDepartamento)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellido;
            FechaNacimiento = fechaNacimiento;
            Direccion = direccion;
            Telefono = telefono;
            Foto = foto;
            IdDepartamento = idDepartamento;
        }
        #endregion

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
        public int IdDepartamento { get; set; }
        //FOTO PUEDE SER ARRAY DE BYTES O STRING SI FUESE UNA URL
        public String Foto { get; set; }
        #endregion
    }
}
