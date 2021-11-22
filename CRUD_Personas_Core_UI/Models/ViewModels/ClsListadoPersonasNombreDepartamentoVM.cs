using CRUD_Personas_BL.Listados;
using CRUD_Personas_Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Personas_Core_UI.Models
{
    public class ClsListadoPersonasNombreDepartamentoVM
    {
        #region propiedades publicas
        public ObservableCollection<ClsPersonaNombreDepartamento> ListadoPersonasDepartamento { get; set; }
        #endregion
        #region constructores
        public ClsListadoPersonasNombreDepartamentoVM()
        {
            ListadoPersonasDepartamento = rellenarListaPersonasDepartamento();
        }
        #endregion
        #region metodos privados
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ClsPersonaNombreDepartamento> rellenarListaPersonasDepartamento()
        {
            ObservableCollection<ClsPersonaNombreDepartamento> listaPersonasDepartamento = new ObservableCollection<ClsPersonaNombreDepartamento>();
            ObservableCollection<ClsPersona> listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            foreach (ClsPersona oPersonaRecogida in listadoPersonas)
            {
                listaPersonasDepartamento.Add(new ClsPersonaNombreDepartamento(
                    oPersonaRecogida.Id, oPersonaRecogida.Nombre, oPersonaRecogida.Apellidos, oPersonaRecogida.FechaNacimiento, oPersonaRecogida.Direccion, oPersonaRecogida.Telefono, oPersonaRecogida.Foto, oPersonaRecogida.IdDepartamento));
            }
            return listaPersonasDepartamento;
        }
        #endregion
    }
}
