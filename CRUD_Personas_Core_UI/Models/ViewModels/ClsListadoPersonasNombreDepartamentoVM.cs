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
    public class ClsListadoPersonasNombreDepartamentoVM
    {
        #region propiedades publicas
        public ObservableCollection<ClsPersonaNombreDepartamento> ListadoPersonasDepartamento { get; set; }
        #endregion
        #region constructores
        public ClsListadoPersonasNombreDepartamentoVM()
        {
            ListadoPersonasDepartamento = rellenarListaPersonasDepartamento2();

        }

        private ObservableCollection<ClsPersonaNombreDepartamento> rellenarListaPersonasDepartamento2()
        {
            ObservableCollection<ClsPersona> listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            ObservableCollection<ClsDepartamento> listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            ObservableCollection<ClsPersonaNombreDepartamento> listadoARetornar = new ObservableCollection<ClsPersonaNombreDepartamento>();
            //Ya tenemos las 2 listas
            foreach(ClsPersona itemPersona in listadoPersonas)
            {
                foreach(ClsDepartamento itemDepartamento in listadoDepartamentos)
                {
                    if(itemPersona.IdDepartamento == itemDepartamento.Id)
                    {
                        listadoARetornar.Add(new ClsPersonaNombreDepartamento(itemPersona, itemDepartamento.Nombre));
                    }
                }
            }

            return listadoARetornar;
        }
        #endregion
        #region metodos privados

        #endregion
    }
}
