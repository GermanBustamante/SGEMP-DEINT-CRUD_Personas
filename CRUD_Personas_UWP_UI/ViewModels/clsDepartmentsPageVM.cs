using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using CRUD_Personas_UWP_UI.ViewModels.Models;
using CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    /// <summary>
    /// VM de la vista DepartamentosPage.xaml
    /// </summary>
    public class clsDepartmentsPageVM : clsVMBase
    {
        #region atributos
        private clsDepartmentListOfPersons oDepartamentoSeleccionadoListadoPersonas;
        private ObservableCollection<ClsDepartamento> listadoDepartamentos;
        private ObservableCollection<ClsPersona> listadoPersonas;
        private DelegateCommand deleteDepartmentCommand;
        private DelegateCommand addDepartmentCommand;
        private DelegateCommand saveDepartmentCommand;
        private static Timer timer;
        private readonly ContentDialog contentDialogDeleteDepartment;
        private bool btnAddPulsado;
        #endregion

        #region constructores
        public clsDepartmentsPageVM()
        {
            ListadoDepartametosConListadoPersonas = new ObservableCollection<clsDepartmentListOfPersons>();
            contentDialogDeleteDepartment = new ContentDialog
            {
                Title = "¿Estás seguro de que quieres borrar este departamento?",
                Content = "Si borras este departamento, no podrás recuperarlo, además, sus empleados irán al Departamento Genérico",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Primary
            };
            try
            {
                listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
                recargarListaDepartamentosYDepartamentoSeleccionado();
            }
            catch (SqlException)
            {
                TxtBlckError = clsPersonsPageVM.MENSAJE_OPERACION_FALLIDA;
                NotifyPropertyChanged("TxtBlckError");
            }

        }
        #endregion

        #region propiedades publicas
        public ObservableCollection<clsDepartmentListOfPersons> ListadoDepartametosConListadoPersonas { get; set; }
        public clsDepartmentListOfPersons ODepartamentoConListadoPersonasSeleccionado
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
                    limpiarTextBoxes(true);//Por si está un mensaje de error y el usuario cambia de item seleccionado, se eliminan los textBoxes
                }
                NotifyPropertyChanged("ODepartamentoConListadoPersonasSeleccionado");
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

        public String TxtBlckMensajeOperacion { get; set; }
        public String TxtBlckError { get; set; }
        #endregion

        #region commands 
        private bool SaveDepartmentCommand_CanExecute()
        {
            //NOTA: Así también controlo que mi departamento genérico no se puede borrar, ya que este tiene Id = 0
            return oDepartamentoSeleccionadoListadoPersonas.Id != 0 || btnAddPulsado;
        }
        private bool DeleteDepartmentCommand_CanExecute()
        {
            //Cuando no haya ningún departamento seleccionado en la vista
            return oDepartamentoSeleccionadoListadoPersonas.Id != 0;
        }

        /// <summary>
        /// Si el nombre del departamento seleccionado es válido, actualiza o añade un departamento a la BD, muestra el mensaje correspondiente con un Timer notificandolo a la vista
        /// recarga la lista de departamentos y el departamento seleccionado, y, al final, deshabilita el botón de guardar de la vista.
        /// En caso contrario, setea el mensaje correspondiente y lo notifica a la vista
        /// </summary>
        private void SaveDepartmentCommand_Execute()
        {
            if (!String.IsNullOrWhiteSpace(oDepartamentoSeleccionadoListadoPersonas.Nombre))
            {
                try
                {
                    //TODO NO FUNCIONA MOSTRAR EL TEXTBOX
                    TxtBlckMensajeOperacion = ClsManejadoraDepartamentoBL.actualizarAniadirDepartamentoBL(ODepartamentoConListadoPersonasSeleccionado) == 1 ? clsPersonsPageVM.MENSAJE_OPERACION_EXITOSA : clsPersonsPageVM.MENSAJE_OPERACION_FALLIDA;
                    NotifyPropertyChanged("TxtBlckMensajeOperacion");
                    setTimer();
                    recargarListaDepartamentosYDepartamentoSeleccionado();
                    btnAddPulsado = false;
                    saveDepartmentCommand.RaiseCanExecuteChanged();
                }
                catch (SqlException)
                {
                    TxtBlckError = clsPersonsPageVM.MENSAJE_OPERACION_FALLIDA;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }
            else
            {
                TxtBlckError = clsPersonsPageVM.MENSAJE_NOMBRE_VACIO;
                NotifyPropertyChanged("TxtBlckError");
            }
        }

        /// <summary>
        /// Habilita el botón de guardar, limpia los textBoxes y limpia de la vista el departamento seleccionado de la lista
        /// </summary>
        private void AddDepartmentCommand_Execute()
        {
            btnAddPulsado = true;
            saveDepartmentCommand.RaiseCanExecuteChanged();
            oDepartamentoSeleccionadoListadoPersonas = new clsDepartmentListOfPersons();
            NotifyPropertyChanged("ODepartamentoConListadoPersonasSeleccionado");
            limpiarTextBoxes(false);
        }

        /// <summary>
        /// Muestra un content dialog preguntando si quiere borrar un departamento, y si ha pulsado el botón de Borrar, elimina dicho departamento
        /// de la BD y de ListadoDepartametosConListadoPersonas, reinicia los textBoxes y comprueba si el botón de guardar se puede habilitar
        /// </summary>
        private async void DeleteDepartmentCommand_Execute()
        {

            var result = await contentDialogDeleteDepartment.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    ClsManejadoraDepartamentoBL.eliminarDepartamentoBL(oDepartamentoSeleccionadoListadoPersonas.Id);
                    ListadoDepartametosConListadoPersonas.Remove(oDepartamentoSeleccionadoListadoPersonas);
                    btnAddPulsado = false;
                    saveDepartmentCommand.RaiseCanExecuteChanged();
                    limpiarTextBoxes(false);
                }
                catch (SqlException)
                {
                    TxtBlckError = clsPersonsPageVM.MENSAJE_OPERACION_FALLIDA;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// Recarga la propiedad ListadoDepartametosConListadoPersonas (lista de departamento en la vista) y oDepartamentoSeleccionadoListadoPersonas notificando a la propiedad de dicha recarga
        /// </summary> 
        private void recargarListaDepartamentosYDepartamentoSeleccionado()
        //NOTA: Me hubiese gustado llamar a este método cada vez que diese una excepción, para limpiar la vista entera una vez dado el error y que solo
        //se muestre el mensaje, pero tendría que controlar una SqlException ya aquí o al que le llame como en las otras llamadas, pudiendo ocasionar un bucle
        //infinito de excepciones. Arreglar si da tiempo
        {
            listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
            if (ListadoDepartametosConListadoPersonas.Count != 0)
            {
                ListadoDepartametosConListadoPersonas.Clear();
            }
            foreach (ClsDepartamento itemDepartamento in listadoDepartamentos)
            {
                ObservableCollection<ClsPersona> listadoPersonasDepartamento = new ObservableCollection<ClsPersona>(from p in listadoPersonas
                                                                                                                    where p.IdDepartamento == itemDepartamento.Id
                                                                                                                    select p);
                //ObservableCollection<ClsPersona> listadoPersonasDepartamento = new ObservableCollection<ClsPersona>(listadoPersonas.Where(x => x.IdDepartamento == item.Id).ToList());
                ListadoDepartametosConListadoPersonas.Add(new clsDepartmentListOfPersons(itemDepartamento, listadoPersonasDepartamento));
            }
            oDepartamentoSeleccionadoListadoPersonas = new clsDepartmentListOfPersons();
            NotifyPropertyChanged("ODepartamentoConListadoPersonasSeleccionado");
        }

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
                TxtBlckMensajeOperacion = "";
                NotifyPropertyChanged("TxtBlckMensajeOperacion");
                timer.Stop();
            });
        }

        /// <summary>
        /// Reinicia los Strings de los textboxes bindeados a la vista y lo notifica a esta, dependiendo del booleano
        /// borra o no el String que notifica cuando se ejecuta una operacion
        /// </summary>
        private void limpiarTextBoxes(bool borrarTxtBlckMensajeOperacion)
        {
            if (borrarTxtBlckMensajeOperacion)
            {
                TxtBlckMensajeOperacion = "";
                NotifyPropertyChanged("TxtBlckMensajeOperacion");
            }

            TxtBlckError = "";
            NotifyPropertyChanged("TxtBlckError");
        }
        #endregion
    }
}
