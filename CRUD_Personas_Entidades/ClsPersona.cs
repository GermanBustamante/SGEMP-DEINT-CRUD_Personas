using System;
using System.ComponentModel.DataAnnotations;

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
        public ClsPersona(int id, string nombre, string apellido, DateTime fechaNacimiento, String direccion, String telefono, String foto, int idDepartamento)
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

        #region propiedades publicas]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre debe ser obligatorio"), MaxLength(30)]
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            { nombre = value; }
        }

        [Required(ErrorMessage = "Los apellidos deben ser obligatorios"), MaxLength(30)]
        public string Apellidos
        {
            get
            { return apellidos; }
            set
            { apellidos = value; }
        }

        //TODO DATAANNOTATION CORRECTO AL METER UNA FECHA INVALIDA
        [DataType(DataType.Date, ErrorMessage = "Debes introducir una fecha")]
        [Required(ErrorMessage = "La fecha de nacimiento debe ser obligatoria")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La dirección debe ser obligatoria"), MaxLength(50)]
        public String Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono debe ser obligatorio"), MaxLength(12),
         RegularExpression("^[0-9]{9,12}$", ErrorMessage = "Debe ser un número de telefono de 9 a 12")]
        public String Telefono { get; set; }

        [Required(ErrorMessage = "El departamento debe ser obligatorio")]
        public int IdDepartamento { get; set; }

        public String Foto { get; set; }
        #endregion
    }
}
