using CRUD_Personas_BL.Listados;
using CRUD_Personas_BL.Manejadoras;
using CRUD_Personas_Entidades;
using CRUD_Personas_UWP_UI.ViewModels.Models;
using CRUD_Personas_UWP_UI.ViewModels.Utilidades;
using GalaSoft.MvvmLight.Views;
using Microsoft.VisualStudio.PlatformUI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsPersonsPageVM : clsVMBase
    {
        #region atributos
        private clsPersonDepartmentName oPersonaSeleccionadaNombreDepartamento;
        private ObservableCollection<ClsDepartamento> listadoDepartamentos;
        private ObservableCollection<clsPersonDepartmentName> listadoPersonasNombreDepartamento;
        private DelegateCommand deletePersonCommand;
        private DelegateCommand addPersonCommand;
        private DelegateCommand savePersonCommand;
        private String txtBlckMensajeOperacion;
        private String txtBlckError;
        private static Timer timer;
        private readonly ContentDialog contentDialogDeletePerson;
        private bool btnAddPulsado = false;
        #endregion

        #region constantes
        public const string MENSAJE_OPERACION_EXITOSA = "La operación ha sido un exito";
        public const string MENSAJE_OPERACION_FALLIDA = "Algo ha fallado";
        #endregion
        #region propiedades publicas
        public DelegateCommand SavePersonCommand
        {
            get
            {
                return savePersonCommand = new DelegateCommand(SavePersonCommand_Execute, SavePersonCommand_CanExecute);
            }
        }

        public DelegateCommand AddPersonCommand
        {
            get
            {
                return addPersonCommand = new DelegateCommand(AddPersonCommand_Execute);
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
                //Cuando borro una persona esta se queda en o edito o algo null ya que la elimino de la lista,  
                oPersonaSeleccionadaNombreDepartamento = value ?? new clsPersonDepartmentName();
                NotifyPropertyChanged("OPersonaSeleccionadaNombreDepartamento");
                deletePersonCommand.RaiseCanExecuteChanged();
                savePersonCommand.RaiseCanExecuteChanged();
            }
        }

        public String TxtBlckMensajeOperacion
        {
            get { return txtBlckMensajeOperacion; }
            set
            {
                txtBlckMensajeOperacion = value;
                NotifyPropertyChanged("TxtBlckMensajeOperacion");
                setTimer();
            }
        }

        public String TxtBlckError
        {
            get { return TxtBlckError; }
            set
            {
                TxtBlckError = value;
                NotifyPropertyChanged("TxtBlckError");
            }
        }
        #endregion
        #region constructores
        public clsPersonsPageVM()
        {
            ListadoPersonasNombreDepartamento = new ObservableCollection<clsPersonDepartmentName>();
            this.contentDialogDeletePerson = instanciarContentDialogDeletePerson();
            try
            {
                listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
                recargarAtributosVM();
            }
            catch (Exception ex) 
            {
                txtBlckError = ex.Message;
                NotifyPropertyChanged("TxtBlckError");
            }
        }
        #endregion

        #region commands
        private bool DeletePersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento.Id != 0;
        }

        private bool SavePersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento.Id != 0 || btnAddPulsado;
        }
        /// <summary>
        /// 
        /// </summary>
        private void AddPersonCommand_Execute()
        {
                btnAddPulsado = true;
                savePersonCommand.RaiseCanExecuteChanged();
                oPersonaSeleccionadaNombreDepartamento = new clsPersonDepartmentName();
                NotifyPropertyChanged("OPersonaSeleccionadaNombreDepartamento");
        }

        /// <summary>
        /// 
        /// </summary>
        private void SavePersonCommand_Execute()
        {
            if (oPersonaSeleccionadaNombreDepartamento.Foto == null)
            {
                oPersonaSeleccionadaNombreDepartamento.Foto = "https://www.pinclipart.com/picdir/middle/393-3932440_png-file-svg-icono-de-contacto-png-blanco.png";
            }
            try
            {
                txtBlckMensajeOperacion = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento) == 1 ? MENSAJE_OPERACION_EXITOSA : MENSAJE_OPERACION_FALLIDA;
                NotifyPropertyChanged("TxtBlckMensajeOperacion");
                setTimer();
                recargarAtributosVM();
                btnAddPulsado = false;
                savePersonCommand.RaiseCanExecuteChanged();
            }
            catch (SqlException ex)
            {
                txtBlckError = ex.Message;
                NotifyPropertyChanged("TxtBlckError");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeletePersonCommand_Execute()
        {
            mostrarContentDialog();
        }
        #endregion

        #region metodos privados
        private void recargarAtributosVM()
        {
            ObservableCollection<ClsPersona> listaPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            if (ListadoPersonasNombreDepartamento != null)
            {
                ListadoPersonasNombreDepartamento.Clear();//Cuando limpio la lista se crea una nueva, es muy raro
            }
            foreach (ClsPersona itemPersona in listaPersonas)
            {
                String nombreDepartamentoItemPersona = (from l in listadoDepartamentos
                                                        where itemPersona.IdDepartamento == l.Id
                                                        select l.Nombre).FirstOrDefault().ToString();
                //String nombreDepartamentoItemPersona = listadoDepartamentos.Where(x => x.Id == itemPersona.IdDepartamento).FirstOrDefault().Nombre;
                ListadoPersonasNombreDepartamento.Add(new clsPersonDepartmentName(itemPersona, nombreDepartamentoItemPersona));
            }
            oPersonaSeleccionadaNombreDepartamento = new clsPersonDepartmentName();
            NotifyPropertyChanged("OPersonaSeleccionadaNombreDepartamento");
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
                txtBlckMensajeOperacion = "";
                NotifyPropertyChanged("TxtBlckMensajeOperacion");
                timer.Stop();
            });
        }

        private ContentDialog instanciarContentDialogDeletePerson()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "¿Estás seguro de que quieres borrar a esta persona?",
                Content = "Si borras esta persona, no podrás recuperarla. ¿Quieres borrarla?",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Primary
            };
            return deleteFileDialog;
        }
        private async void mostrarContentDialog()
        {
            var result = await instanciarContentDialogDeletePerson().ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    ClsManejadoraPersonaBL.eliminarPersonaBL(oPersonaSeleccionadaNombreDepartamento.Id);
                    ListadoPersonasNombreDepartamento.Remove(oPersonaSeleccionadaNombreDepartamento);
                    btnAddPulsado = false;
                    savePersonCommand.RaiseCanExecuteChanged();
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
