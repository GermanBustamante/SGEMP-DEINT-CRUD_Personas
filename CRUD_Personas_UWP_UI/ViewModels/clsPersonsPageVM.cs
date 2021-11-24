using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using Microsoft.VisualStudio.PlatformUI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsPersonsPageVM : clsVMBase
    {
        //Mi vista necesita una lista de personas y la persona seleccionada(ClsPersonaNombreDepartamento), 

        #region atributos
        private ClsPersona oPersonaSeleccionada;
        private String nombreDepartamento;
        private DelegateCommand deletePersonCommand;
        private DelegateCommand editPersonCommand;
        private DelegateCommand addPersonCommand;
        #endregion
        #region propiedades publicas
        public DelegateCommand AddPersonCommand
        {
            get
            {
                return addPersonCommand = new DelegateCommand(AddPersonCommand_Execute, AddPersonCommand_CanExecute);
            }
        }

        

        public DelegateCommand EditPersonCommand
        {
            get
            {
                return editPersonCommand = new DelegateCommand(EditPersonCommand_Execute, EditPersonCommand_CanExecute);
            }
        }
        
        public DelegateCommand DeletePersonCommand
        {
            get
            {
                return deletePersonCommand = new DelegateCommand(DeletePersonCommand_Execute, DeletePersonCommand_CanExecute);
            }
        }

        public ObservableCollection<ClsPersona> ListadoPersonas { get; set; }
        public ObservableCollection<String> ListadoNombresDepartamentos { get; set; }
        public String NombreDepartamento
        {
            get { return nombreDepartamento; }
            set//SI LLEGO A UNA PERSONA Y LE DOY A CAMBIAR, SE CAMBIA PERO AL SALTAR A OTRA PERSONA NO VUELVE A SU VALOR
            {
                nombreDepartamento = value;
                NotifyPropertyChanged("NombreDepartamento");
            }
        }
        public ClsPersona OPersonaSeleccionada
        {
            get { return oPersonaSeleccionada; }
            set
            {
                oPersonaSeleccionada = value;
                //ESTE IF-ELSE ES PQ SI BORRO UNA PERSONA, LUEGO OPERSONASELECCIONADA SE PONE A NULL Y DEBO
                //CONTROLAR QUE SI ESO OCURRE EL NOMBRE DEPARTAMENTO TAMBIÉN SE PONGA NULO PARA QUE AL BORRAR NO
                //SE QUEDE NINGÚN DEPARTAMENTO SELECCIONADO
                NombreDepartamento = oPersonaSeleccionada != null ? ClsListadoDepartamentosBL.getNombreDepartamentoBL(oPersonaSeleccionada.IdDepartamento) : null;

                NotifyPropertyChanged("OPersonaSeleccionada");
                editPersonCommand.RaiseCanExecuteChanged();
                deletePersonCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region constructores
        public clsPersonsPageVM()
        {
            ListadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            ListadoNombresDepartamentos = ClsListadoDepartamentosBL.getListadoNombresDepartamentosBL();
        }
        #endregion

        #region commands
        

        private void AddPersonCommand_Execute()
        {
            //TODO NI ZORRA DE COMO HACERLO AQUÍ
        }
        private bool AddPersonCommand_CanExecute()
        {
            return oPersonaSeleccionada == null;
        }
        private void DeletePersonCommand_Execute()
        {
            //¿DUDA QUE HAGO CON EL INT QUE DEVUELVE? ESTA BIEN ASÍ
            ClsManejadoraPersonsaBL.eliminarPersonaBL(oPersonaSeleccionada.Id);
            ListadoPersonas.Remove(oPersonaSeleccionada);
        }
        private bool DeletePersonCommand_CanExecute()
        {
            return oPersonaSeleccionada != null;
        }
        private void EditPersonCommand_Execute()
        {
            ClsManejadoraPersonsaBL.actualizarPersonaBL(oPersonaSeleccionada);
        }
        private bool EditPersonCommand_CanExecute()
        {
            return oPersonaSeleccionada != null;
        }
        #endregion
    }
}
