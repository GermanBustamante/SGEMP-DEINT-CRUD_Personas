using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;

namespace CRUD_Personas_Core_UI.Models
{
    /// <summary>
    /// Clase persona que solo contendrá las propiedades de una persona que quiero mostrar en mi vista de listado de personas,
    /// Nombre, Apellidos, IdDepartamento y nombre del departamento
    /// </summary>
    public class ClsPersonaNombreApellidosNombreDepartamento
    {
        #region propiedades publicas
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        #endregion

        #region constructores
        public ClsPersonaNombreApellidosNombreDepartamento() { }
        public ClsPersonaNombreApellidosNombreDepartamento(int id, string nombre, string apellidos, int idDepartamento)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            IdDepartamento = idDepartamento;
            NombreDepartamento = ClsListadoDepartamentosBL.getNombreDepartamentoBL(idDepartamento);
        }

        //Este constructor lo usaremos cuando usamos una lista de objetos de esta clase, para que no busque reiteradas
        //veces en la BD, sino que su recoge el que lo llama la lista departamentos y le devuelve el suyo
        public ClsPersonaNombreApellidosNombreDepartamento(ClsPersona oPersona, string nombreDepartamento)
        {
            Id = oPersona.Id;
            Nombre = oPersona.Nombre;
            Apellidos = oPersona.Apellidos;
            IdDepartamento = oPersona.IdDepartamento;
            NombreDepartamento = nombreDepartamento;
        }
        #endregion
    }
}
