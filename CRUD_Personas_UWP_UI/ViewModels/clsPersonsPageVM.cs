using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using CRUD_Personas_UWP_UI.ViewModels.Models;
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
        #region atributos
        private clsPersonDepartmentName oPersonaSeleccionadaNombreDepartamento;
        private ObservableCollection<ClsDepartamento> listadoDepartamentos;
        private ObservableCollection<clsPersonDepartmentName> listadoPersonasNombreDepartamento;
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

        public ObservableCollection<clsPersonDepartmentName> ListadoPersonasNombreDepartamento
        {
            get { return listadoPersonasNombreDepartamento; }
            set
            {
                listadoPersonasNombreDepartamento = value;
                NotifyPropertyChanged("ListadoPersonasNombreDepartamento");
            }
        }
        public ObservableCollection<ClsDepartamento> ListadoDepartamentos
        {
            get { return listadoDepartamentos; }
            set
            {
                listadoDepartamentos = value;
            }
        }
        public clsPersonDepartmentName OPersonaSeleccionadaNombreDepartamento
        {
            get { return oPersonaSeleccionadaNombreDepartamento; }
            set
            {
                oPersonaSeleccionadaNombreDepartamento = value;
                NotifyPropertyChanged("OPersonaSeleccionadaNombreDepartamento");
                editPersonCommand.RaiseCanExecuteChanged();
                deletePersonCommand.RaiseCanExecuteChanged();
                addPersonCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region constructores
        public clsPersonsPageVM()
        {
            ListadoPersonasNombreDepartamento = new ObservableCollection<clsPersonDepartmentName>();
            ObservableCollection<ClsPersona> listaPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            foreach (var item in listaPersonas)
            {
                ListadoPersonasNombreDepartamento.Add(new clsPersonDepartmentName(item));
            }
            listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
        }
        #endregion

        #region commands
        private void AddPersonCommand_Execute()
        {
            ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento);
        }
        private bool AddPersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento == null;
        }
        private void DeletePersonCommand_Execute()
        {
            //TODO QUE ES MEJOR QUE LE DIGA QUE CUANDO BORRE UNA PERSONA VUELVA A MIRAR LA LISTA DE PERSONAS
            //O MEJOR LO BORRO DE LA QUE TENGO Y YASTA??
            ClsManejadoraPersonsaBL.eliminarPersonaBL(oPersonaSeleccionadaNombreDepartamento.Id);
            ListadoPersonasNombreDepartamento.Remove(oPersonaSeleccionadaNombreDepartamento);
        }
        private bool DeletePersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento != null;
        }
        private void EditPersonCommand_Execute()
        {
            ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento);
        }
        private bool EditPersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento != null;
        }
        #endregion
    }
}
