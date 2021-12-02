using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Models
{
    /// <summary>
    /// ViewModel de mi pagina index de mi PersonasController, ya que solo necesito una lista de personas
    /// que tengan un Id, nombre, apellidos, idDepartametento y el nombre de este
    /// </summary>
    public class ClsListadoPersonasNombreApellidosNombreDepartamentoVM
    {
        #region propiedades publicas
        public ObservableCollection<ClsPersonaNombreApellidosNombreDepartamento> ListadoPersonasNombreApellidosNombreDepartamento { get; set; }
        #endregion
        #region constructores
        public ClsListadoPersonasNombreApellidosNombreDepartamentoVM()
        {
            ListadoPersonasNombreApellidosNombreDepartamento = rellenarListadoPersonas();
        }
        #endregion
        #region metodos privados
        /// <summary>
        /// Obtiene una lista de departamentos y otra de personas de la capa DAL, luego, los recorre para generar objetos
        /// tipo ClsPersonaNombreApellidosNombreDepartamento, los añade a una lista y la retorna
        /// </summary>
        /// <returns>ObservableCollection*ClsPersonaNombreApellidosNombreDepartamento* siendo este el listado a retornar con las personas y su nombre departamento</returns>
        private ObservableCollection<ClsPersonaNombreApellidosNombreDepartamento> rellenarListadoPersonas()
        {
            ObservableCollection<ClsPersona> listadoPersonas = ClsListadoPersonasBL.getListadoPersonasIdNombreApellidosIdDepartamentoBL();
            ObservableCollection<ClsDepartamento> listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            ObservableCollection<ClsPersonaNombreApellidosNombreDepartamento> listadoARetornar = new ObservableCollection<ClsPersonaNombreApellidosNombreDepartamento>();
            foreach (ClsPersona itemPersona in listadoPersonas)
            {
                String nombreDepartamentoItemPersona = (from l in listadoDepartamentos
                                                       where itemPersona.IdDepartamento == l.Id
                                                       select l.Nombre).FirstOrDefault().ToString();
                listadoARetornar.Add(new ClsPersonaNombreApellidosNombreDepartamento(itemPersona, nombreDepartamentoItemPersona));
            }
            return listadoARetornar;
        }
        #endregion
    }
}
