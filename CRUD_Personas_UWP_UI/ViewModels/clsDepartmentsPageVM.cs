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
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsDepartmentsPageVM : clsVMBase
    {
        #region atributos
        private clsDepartmentListOfPersons oDepartamentoSeleccionadoListadoPersonas;
        private ObservableCollection<ClsDepartamento> listadoDepartamentos;
        private ObservableCollection<ClsPersona> listadoPersonas;
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
            contentDialogDeleteDepartment = new ContentDialog
            {
                Title = "¿Estás seguro de que quieres borrar este departamento?",
                Content = "Si borras este departamento, no podrás recuperarlo, además, sus empleados irán al Departamento Genérico",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Primary
            };
            listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            ListadoDepartametosPersonas = new ObservableCollection<clsDepartmentListOfPersons>();
            recargarListaYSeleccion();
        }

        private void recargarListaYSeleccion()
        {
            listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            if (ListadoDepartametosPersonas != null)
            {
                ListadoDepartametosPersonas.Clear();
            }
            foreach (ClsDepartamento itemDepartamento in listadoDepartamentos)
            {
                ObservableCollection<ClsPersona> listadoPersonasDepartamento = new ObservableCollection<ClsPersona>(from p in listadoPersonas
                                                                                                                    where p.IdDepartamento == itemDepartamento.Id
                                                                                                                    select p);
                //ObservableCollection<ClsPersona> listadoPersonasDepartamento = new ObservableCollection<ClsPersona>(listadoPersonas.Where(x => x.IdDepartamento == item.Id).ToList());
                ListadoDepartametosPersonas.Add(new clsDepartmentListOfPersons(itemDepartamento, listadoPersonasDepartamento));
            }
            oDepartamentoSeleccionadoListadoPersonas = new clsDepartmentListOfPersons();
            NotifyPropertyChanged("ODepartamentoSeleccionadoListadoPersonas");
        }
        #endregion

        #region propiedades publicas
        public ObservableCollection<clsDepartmentListOfPersons> ListadoDepartametosPersonas { get; set; }
        public clsDepartmentListOfPersons ODepartamentoSeleccionadoListadoPersonas
        {
            get { return oDepartamentoSeleccionadoListadoPersonas; }
            set
            {
                if (value != null)
                {
                    oDepartamentoSeleccionadoListadoPersonas = value;
                }
                else
                {
                    oDepartamentoSeleccionadoListadoPersonas = new clsDepartmentListOfPersons();
                    reinicarTextBoxes();//Por si está un mensaje de error y el usuario cambia de item seleccionado
                }
                NotifyPropertyChanged("ODepartamentoSeleccionadoListadoPersonas");
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

        public String TxtBlckMensajeOperacion
        {
            get { return txtBlckMensajeOperacion; }
            set
            { txtBlckMensajeOperacion = value; }
        }

        public String TxtBlckError
        {
            get { return txtBlckError; }
            set { txtBlckError = value; }
        }
        #endregion
        #region commands 
        private bool SaveDepartmentCommand_CanExecute()
        {
            return oDepartamentoSeleccionadoListadoPersonas.Id != 0 || btnAddPulsado;
        }
        private bool DeleteDepartmentCommand_CanExecute()
        {
            return oDepartamentoSeleccionadoListadoPersonas.Id != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveDepartmentCommand_Execute()
        {
            if (!String.IsNullOrWhiteSpace(oDepartamentoSeleccionadoListadoPersonas.Nombre))
            {
                try
                {
                    txtBlckMensajeOperacion = ClsManejadoraDepartamentoBL.actualizarAniadirDepartamentoBL(ODepartamentoSeleccionadoListadoPersonas) == 1 ? clsPersonsPageVM.MENSAJE_OPERACION_EXITOSA : clsPersonsPageVM.MENSAJE_OPERACION_FALLIDA;
                    NotifyPropertyChanged("TxtBlckMensajeOperacion");
                    setTimer();
                    recargarListaYSeleccion();
                    btnAddPulsado = false;
                    saveDepartmentCommand.RaiseCanExecuteChanged();
                }
                catch (SqlException ex)
                {
                    txtBlckError = ex.Message;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }
            else
            {
                txtBlckError = "No se puede dejar el nombre vacío";
                NotifyPropertyChanged("TxtBlckError");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void AddDepartmentCommand_Execute()
        {
            btnAddPulsado = true;
            saveDepartmentCommand.RaiseCanExecuteChanged();
            oDepartamentoSeleccionadoListadoPersonas = new clsDepartmentListOfPersons();
            NotifyPropertyChanged("ODepartamentoSeleccionadoListadoPersonas");
            reinicarTextBoxes();
        }
        /// <summary>
        /// 
        /// </summary>
        private async void DeleteDepartmentCommand_Execute()
        {

            var result = await contentDialogDeleteDepartment.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    ClsManejadoraDepartamentoBL.eliminarDepartamentoBL(oDepartamentoSeleccionadoListadoPersonas.Id);
                    ListadoDepartametosPersonas.Remove(oDepartamentoSeleccionadoListadoPersonas);
                    btnAddPulsado = false;
                    saveDepartmentCommand.RaiseCanExecuteChanged();
                    reinicarTextBoxes();
                }
                catch (SqlException ex)
                {
                    txtBlckError = ex.Message;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }
        }
        #endregion
        #region metodos privados

        /// <summary>
        /// <b>Prototipo:</b> private void setTimer()<br/>
        /// <b>Comentarios:</b> Método que setea los atributos del objeto tipo Timer<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Instancia el objeto tipo Timer con 5 segundos y crea su evento, además, comienza dicho objeto
        /// </summary>
        private void setTimer()
        {
            timer = new Timer(5000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        /// <summary>
        /// <b>Prototipo:</b> private void Timer_Elapsed(object sender, ElapsedEventArgs e)<br/>
        /// <b>Comentarios:</b> Evento asociado a ocurrir cada 5 segundos en el timer<br/>
        /// <b>Precondiciones:</b> ninguna<br/>
        /// <b>Postcondiciones:</b> Mediante un método de sincronía, invisibiliza el txtBlockDatosGuardados y detiene el timer para que solo ocurra una vez
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtBlckMensajeOperacion = "";
                NotifyPropertyChanged("TxtBlckMensajeOperacion");
                timer.Stop();
            });
        }

        /// <summary>
        /// Reinicia los textBoxes de la vista y lo notifica a esta
        /// </summary>
        private void reinicarTextBoxes()
        {
            txtBlckMensajeOperacion = "";
            txtBlckError = "";
            NotifyPropertyChanged("TxtBlckMensajeOperacion");
            NotifyPropertyChanged("TxtBlckError");
        }
        #endregion
    }
}
