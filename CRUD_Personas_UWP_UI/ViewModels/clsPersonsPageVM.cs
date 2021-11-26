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
        private DelegateCommand savePersonCommand;
        #endregion
        #region propiedades publicas
        public DelegateCommand SavePersonCommand
        {
            get
            {
                return savePersonCommand = new DelegateCommand(SavePersonCommand_Execute, SavePersonCommand_CanExecute);
            }
        }

        private bool SavePersonCommand_CanExecute()
        {
            throw new NotImplementedException();
        }

        private void SavePersonCommand_Execute()
        {
            throw new NotImplementedException();
        }

        public DelegateCommand AddPersonCommand
        {
            get
            {
                return addPersonCommand = new DelegateCommand(AddPersonCommand_Execute);
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
                if (value != null)
                {
                    oPersonaSeleccionadaNombreDepartamento = value;
                    NotifyPropertyChanged("OPersonaSeleccionadaNombreDepartamento");
                    editPersonCommand.RaiseCanExecuteChanged();
                    deletePersonCommand.RaiseCanExecuteChanged();
                    addPersonCommand.RaiseCanExecuteChanged();
                }

            }
        }
        #endregion

        #region constructores
        public clsPersonsPageVM()
        {
            ListadoPersonasNombreDepartamento = new ObservableCollection<clsPersonDepartmentName>();
            listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            recargarAtributosVM();
        }

        private void recargarAtributosVM()
        {
            ObservableCollection<ClsPersona> listaPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            if (ListadoPersonasNombreDepartamento != null)
            {
                ListadoPersonasNombreDepartamento.Clear();
            }
            foreach (var item in listaPersonas)
            {
                ListadoPersonasNombreDepartamento.Add(new clsPersonDepartmentName(item));
            }
            oPersonaSeleccionadaNombreDepartamento = new clsPersonDepartmentName();
        }
        #endregion

        #region commands
        private void AddPersonCommand_Execute()
        {
            oPersonaSeleccionadaNombreDepartamento = new clsPersonDepartmentName();
            
            oPersonaSeleccionadaNombreDepartamento.Foto = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.xatakandroid.com%2Fsistema-operativo%2Fse-actualizara-mi-movil-a-android-10-lista-completa-actualizada&psig=AOvVaw2GoFIeULBEYpxJlDeAqMWb&ust=1637324751968000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCKDBg9fzofQCFQAAAAAdAAAAABAD";

            ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento);
            recargarAtributosVM();
        }
        private bool AddPersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento.Id == 0;
        }
        private void DeletePersonCommand_Execute()
        {
            //TODO QUE ES MEJOR QUE LE DIGA QUE CUANDO BORRE UNA PERSONA VUELVA A MIRAR LA LISTA DE PERSONAS
            //O MEJOR LO BORRO DE LA QUE TENGO Y YASTA??
            ClsManejadoraPersonsaBL.eliminarPersonaBL(oPersonaSeleccionadaNombreDepartamento.Id);
            ListadoPersonasNombreDepartamento.Remove(oPersonaSeleccionadaNombreDepartamento);
            recargarAtributosVM();
        }
        private bool DeletePersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento.Id != 0;
        }
        private void EditPersonCommand_Execute()
        {
            ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento);
            recargarAtributosVM();
        }
        private bool EditPersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento.Id != 0;
        }
        #endregion
    }
}
