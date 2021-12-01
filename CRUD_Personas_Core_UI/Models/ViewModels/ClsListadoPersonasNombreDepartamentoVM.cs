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
            ListadoPersonasDepartamento = rellenarListaPersonasDepartamento();
        }
        #endregion
        #region metodos privados
        private ObservableCollection<ClsPersonaNombreDepartamento> rellenarListaPersonasDepartamento()
        {
            ObservableCollection<ClsPersona> listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            ObservableCollection<ClsDepartamento> listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            ObservableCollection<ClsPersonaNombreDepartamento> listadoARetornar = new ObservableCollection<ClsPersonaNombreDepartamento>();
            //TODO PREGUNTAR A FERNANDO LO QUE ESTÁ COMENTADO
            foreach (ClsPersona itemPersona in listadoPersonas)
            {
                String nombreDepartamentoItemPersona = (from l in listadoDepartamentos
                                                       where itemPersona.IdDepartamento == l.Id
                                                       select l.Nombre).FirstOrDefault().ToString();
                listadoARetornar.Add(new ClsPersonaNombreDepartamento(itemPersona, nombreDepartamentoItemPersona));
                //listadoARetornar = (ObservableCollection<ClsPersonaNombreDepartamento>)
                //    (from d in listadoDepartamentos
                //     join p in listadoPersonas
                //        on d.Id equals p.IdDepartamento
                //     select new ClsPersonaNombreDepartamento(new ClsPersona(p.Id, p.Nombre, p.Apellidos, p.FechaNacimiento, p.Direccion, p.Telefono, p.Foto, p.IdDepartamento), d.Nombre));
                //listadoARetornar.Add(new ClsPersonaNombreDepartamento(itemPersona, listadoDepartamentos.First(department => department.Id == itemPersona.Id).Nombre));
            }
            return listadoARetornar;
        }
        #endregion
    }
}
