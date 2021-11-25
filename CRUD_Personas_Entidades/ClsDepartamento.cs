using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Personas_Entidades
{
    public class ClsDepartamento
    {
        #region propiedades publicas
        public int Id { get; set; }
        public String Nombre { get; set; }
        #endregion
        #region constructores
        public ClsDepartamento() { }
        public ClsDepartamento(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        #endregion
    }
}
