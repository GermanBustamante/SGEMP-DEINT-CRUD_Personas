using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using CRUD_Personas_UWP_UI.ViewModels.Models;
using CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsDepartmentsPageVM : clsVMBase
    {
        #region atributos
        private clsDepartmentListOfPersons oDepartamentoListadoPersonasSeleccionado;
        private DelegateCommand deleteDepartmentCommand;
        private DelegateCommand addDepartmentCommand;
        private DelegateCommand saveDepartmentCommand;
        private String txtBlckMensajeOperacion;
        private String txtBlckError;
        private static Timer timer;
        private readonly ContentDialog contentDialogDeleteDepartment;
        private bool btnAddPulsado;
        #endregion
        #region constructores
        public clsDepartmentsPageVM()
        {
            contentDialogDeleteDepartment = instanciarContentDialogDeleteDepartment();
            ListadoDepartametosPersonas = new ObservableCollection<clsDepartmentListOfPersons>();

            ObservableCollection<ClsDepartamento> listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            ObservableCollection<ClsPersona> listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            foreach (ClsDepartamento item in listadoDepartamentos)
            {
                ObservableCollection<ClsPersona> listadoPersonasDepartamento = new ObservableCollection<ClsPersona>(from p in listadoPersonas
                                                                                                                    where p.IdDepartamento == item.Id
                                                                                                                    select p);
                //ObservableCollection<ClsPersona> listadoPersonasDepartamento = new ObservableCollection<ClsPersona>(listadoPersonas.Where(x => x.IdDepartamento == item.Id).ToList());
                ListadoDepartametosPersonas.Add(new clsDepartmentListOfPersons(item, listadoPersonasDepartamento));
            }
            oDepartamentoListadoPersonasSeleccionado = new clsDepartmentListOfPersons();
            NotifyPropertyChanged("ODepartamentoListadoPersonasSeleccionado");
        }
        #endregion

        #region propiedades publicas
        public ObservableCollection<clsDepartmentListOfPersons> ListadoDepartametosPersonas { get; set; }
        public clsDepartmentListOfPersons ODepartamentoListadoPersonasSeleccionado
        {
            get { return oDepartamentoListadoPersonasSeleccionado; }
            set
            {
                oDepartamentoListadoPersonasSeleccionado = value ?? new clsDepartmentListOfPersons();
                NotifyPropertyChanged("ODepartamentoListadoPersonasSeleccionado");
                saveDepartmentCommand.RaiseCanExecuteChanged();
                deleteDepartmentCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SaveDepartmentCommand
        {
            get
            {
                return saveDepartmentCommand = new DelegateCommand(SaveDepartmentCommand_Execute, SaveDepartmentCommand_CanExecute);
            }
        }
        public DelegateCommand DeleteDepartmentCommand
        {
            get
            {
                return deleteDepartmentCommand = new DelegateCommand(DeleteDepartmentCommand_Execute, DeleteDepartmentCommand_CanExecute);
            }
        }
        public DelegateCommand AddDepartmentCommand
        {
            get
            {
                return addDepartmentCommand = new DelegateCommand(AddDepartmentCommand_Execute);
            }
        }
        #endregion
        #region commands 
        private bool SaveDepartmentCommand_CanExecute()
        {
            return oDepartamentoListadoPersonasSeleccionado.Id != 0 || btnAddPulsado;

        }
        private bool DeleteDepartmentCommand_CanExecute()
        {
            return oDepartamentoListadoPersonasSeleccionado.Id != 0;
        }

        private void SaveDepartmentCommand_Execute()
        {

        }
        private void AddDepartmentCommand_Execute()
        {
            btnAddPulsado = true;
            oDepartamentoListadoPersonasSeleccionado = new clsDepartmentListOfPersons();
            NotifyPropertyChanged("ODepartamentoListadoPersonasSeleccionado");
            saveDepartmentCommand.RaiseCanExecuteChanged();
        }
        private void DeleteDepartmentCommand_Execute()
        {
            mostrarContentDialog();
        }
        #endregion
        #region metodos privados
        private ContentDialog instanciarContentDialogDeleteDepartment()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "¿Estás seguro de que quieres borrar este departamento?",
                Content = "Si borras este departamento, no podrás recuperarlo.",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Primary
            };
            return deleteFileDialog;
        }

        private async void mostrarContentDialog()
        {
            var result = await instanciarContentDialogDeleteDepartment().ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    //HAY Q TRATAR CON TODAS LAS PERSONAS QUE TENGAN UN DEPARTAMENTO
                    ClsManejadoraDepartamentoBL.eliminarDepartamentoBL(oDepartamentoListadoPersonasSeleccionado.Id);
                    ListadoDepartametosPersonas.Remove(oDepartamentoListadoPersonasSeleccionado);
                    btnAddPulsado = false;
                    saveDepartmentCommand.RaiseCanExecuteChanged();
                }
                catch (SqlException ex)
                {
                    txtBlckError = ex.Message;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }
        }
        #endregion
    }
}
