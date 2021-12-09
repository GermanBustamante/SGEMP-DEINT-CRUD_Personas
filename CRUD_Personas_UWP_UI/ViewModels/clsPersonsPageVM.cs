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
        private ObservableCollection<ClsPersona> listadoPersonas;
        private clsDelegateCommand deletePersonCommand;
        private clsDelegateCommand addPersonCommand;
        private clsDelegateCommand savePersonCommand;
        private String txtBlckMensajeOperacion;
        private String txtBlckError;
        private static Timer timer;
        private readonly ContentDialog contentDialogDeletePerson;
        private bool btnAddPulsado = false;
        #endregion

        #region constantes
        public const string MENSAJE_OPERACION_EXITOSA = "La operación ha sido un exito";
        public const string MENSAJE_OPERACION_FALLIDA = "Algo ha fallado";
        public const string MENSAJE_NOMBRE_VACIO = "No puedes dejar el nombre vacío";
        public const string MENSAJE_APELLIDOS_VACIO = "No puedes dejar los apellidos vacíos";
        public const string MENSAJE_FECHA_NACIMIENTO_VACIO = "No puedes dejar la fecha de nacimiento vacía";
        public const string MENSAJE_DIRECCION_VACIO = "No puedes dejar la dirección vacía";
        public const string MENSAJE_TELEFONO_VACIO = "No puedes dejar el teléfono vacío";
        public const string MENSAJE_CAMPO_VACIO = "No puedes dejar ningún campo vacío";
        public const string MENSAJE_NOMBRE_APELLIDOS_VACIOS = "No puedes dejar ni el nombre ni los apellidos vacios";
        #endregion
        #region propiedades publicas
        public clsDelegateCommand SavePersonCommand
        {
            get
            {
                return savePersonCommand = new clsDelegateCommand(SavePersonCommand_Execute, SavePersonCommand_CanExecute);
            }
        }

        public clsDelegateCommand AddPersonCommand
        {
            get
            {
                return addPersonCommand = new clsDelegateCommand(AddPersonCommand_Execute);
            }
        }

        public clsDelegateCommand DeletePersonCommand
        {
            get
            {
                return deletePersonCommand = new clsDelegateCommand(DeletePersonCommand_Execute, DeletePersonCommand_CanExecute);
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
                if (value == null)
                {
                    oPersonaSeleccionadaNombreDepartamento = new clsPersonDepartmentName();
                }
                else
                {
                    oPersonaSeleccionadaNombreDepartamento = value;
                    //Por si está un mensaje de error y el usuario cambia de item seleccionado
                    reinicarTextBoxes();
                }
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
            get { return txtBlckError; }
            set
            {
                txtBlckError = value;
                NotifyPropertyChanged("TxtBlckError");
            }
        }
        #endregion
        #region constructores
        public clsPersonsPageVM()
        {
            ListadoPersonasNombreDepartamento = new ObservableCollection<clsPersonDepartmentName>();
            this.contentDialogDeletePerson = new ContentDialog
            {
                Title = "¿Estás seguro de que quieres borrar a esta persona?",
                Content = "Si borras esta persona, no podrás recuperarla.",
                PrimaryButtonText = "Borrar",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Primary
            }; 
            try
            {
                listadoDepartamentos = ClsListadoDepartamentosBL.getListadoDepartamentosBL();
                recargarListaYPersonaSeleccionada();
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
            reinicarTextBoxes();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SavePersonCommand_Execute()
        {
            if (esPersonaCompleta())
            {
                //Setea una foto por defecto si se ha dejado vacío
                if (oPersonaSeleccionadaNombreDepartamento.Foto == null || String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Foto))
                {
                    oPersonaSeleccionadaNombreDepartamento.Foto = "https://www.pinclipart.com/picdir/middle/393-3932440_png-file-svg-icono-de-contacto-png-blanco.png";
                }
                try
                {
                    txtBlckMensajeOperacion = ClsManejadoraPersonaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento) == 1 ? MENSAJE_OPERACION_EXITOSA : MENSAJE_OPERACION_FALLIDA;
                    NotifyPropertyChanged("TxtBlckMensajeOperacion");
                    setTimer();
                    recargarListaYPersonaSeleccionada();
                    btnAddPulsado = false;
                    savePersonCommand.RaiseCanExecuteChanged();
                }
                catch (SqlException ex)
                {
                    txtBlckError = ex.Message;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Nombre) && !String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Apellidos))
                {
                    txtBlckError = MENSAJE_NOMBRE_VACIO;
                    NotifyPropertyChanged("TxtBlckError");
                }
                else if (String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Apellidos) && !String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Nombre))
                {
                    txtBlckError = MENSAJE_APELLIDOS_VACIO;
                    NotifyPropertyChanged("TxtBlckError");
                }
                else
                {
                    txtBlckError = MENSAJE_NOMBRE_APELLIDOS_VACIOS;
                    NotifyPropertyChanged("TxtBlckError");
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private async void DeletePersonCommand_Execute()
        {
            var result = await contentDialogDeletePerson.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    ClsManejadoraPersonaBL.eliminarPersonaBL(oPersonaSeleccionadaNombreDepartamento.Id);
                    ListadoPersonasNombreDepartamento.Remove(oPersonaSeleccionadaNombreDepartamento);
                    btnAddPulsado = false;
                    savePersonCommand.RaiseCanExecuteChanged();
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
        /// Recoge un listado de personas de la BD, la limpia si ya habia algo y lo vuelve a dar
        /// </summary>
        private void recargarListaYPersonaSeleccionada()
        {
            listadoPersonas = ClsListadoPersonasBL.getListadoPersonasCompletoBL();
            if (ListadoPersonasNombreDepartamento != null)
            {
                ListadoPersonasNombreDepartamento.Clear();
            }
            foreach (ClsPersona itemPersona in listadoPersonas)
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

        /// <summary>
        /// Devuelve true si la persona seleccionada en ka lista tiene nombres y apellidos no vacios, en caso contrario
        /// devuelve false
        /// </summary>
        /// <returns></returns>
        private bool esPersonaCompleta()
        {
            return !(String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Nombre) || String.IsNullOrWhiteSpace(oPersonaSeleccionadaNombreDepartamento.Apellidos));
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
