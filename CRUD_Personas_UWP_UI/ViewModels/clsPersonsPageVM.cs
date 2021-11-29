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
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace CRUD_Personas_UWP_UI.ViewModels
{
    public class clsPersonsPageVM : clsVMBase
    {
        #region atributos
        private clsPersonDepartmentName oPersonaSeleccionadaNombreDepartamento;
        private ObservableCollection<ClsDepartamento> listadoDepartamentos;
        private ObservableCollection<clsPersonDepartmentName> listadoPersonasNombreDepartamento;
        private DelegateCommand deletePersonCommand;
        //private DelegateCommand editPersonCommand;
        private DelegateCommand addPersonCommand;
        private DelegateCommand savePersonCommand;
        private String txtBlckOperacionExitosa;
        private static Timer timer;
        #endregion
        public const string MENSAJE_OPERACION_EXITOSA = "La operación ha sido un exito";
        public const string MENSAJE_OPERACION_FALLIDA = "Algo ha fallado";

        #region constantes

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
        //public DelegateCommand EditPersonCommand
        //{
        //    get
        //    {
        //        return editPersonCommand = new DelegateCommand(EditPersonCommand_Execute, EditPersonCommand_CanExecute);
        //    }
        //}
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
        public String TxtBlckOperacionExitosa
        {
            get { return txtBlckOperacionExitosa; }
            set
            {
                txtBlckOperacionExitosa = value;
                NotifyPropertyChanged("TxtBlckOperacionExitosa");
                setTimer();
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
        #endregion
      

        #region commands
        private bool DeletePersonCommand_CanExecute()
        {
            return oPersonaSeleccionadaNombreDepartamento.Id != 0;
        }

        private bool SavePersonCommand_CanExecute()
        {
            return true;//oPersonaSeleccionadaNombreDepartamento.Id != 0 /* || QUE CONDICION LE PONGO  */;
        }

        //TODO DUDA CUANDO QUIERO AGREGAR UNA PERSONA LE DIGO Q EL ID SEA != 0 (NO HAY PERSONA SELECCIONADA) PERO YO QUIERO ESA CONDICIÓN Y OTRA 
        //MÁS PARA QUE ESA PERSONA NO ME LA ENVIE
        private void AddPersonCommand_Execute()
        {
            recargarAtributosVM();
        }
        private void SavePersonCommand_Execute()
        {
            oPersonaSeleccionadaNombreDepartamento.Foto = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.xatakandroid.com%2Fsistema-operativo%2Fse-actualizara-mi-movil-a-android-10-lista-completa-actualizada&psig=AOvVaw2GoFIeULBEYpxJlDeAqMWb&ust=1637324751968000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCKDBg9fzofQCFQAAAAAdAAAAABAD";
            TxtBlckOperacionExitosa = ClsManejadoraPersonsaBL.actualizarAñadirPersonaBL(oPersonaSeleccionadaNombreDepartamento) == 1 ? MENSAJE_OPERACION_EXITOSA : MENSAJE_OPERACION_FALLIDA;
            recargarAtributosVM();
        }
        private void DeletePersonCommand_Execute()
        {
            ClsManejadoraPersonsaBL.eliminarPersonaBL(oPersonaSeleccionadaNombreDepartamento.Id);
            ListadoPersonasNombreDepartamento.Remove(oPersonaSeleccionadaNombreDepartamento);
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
            foreach (var item in listaPersonas)
            {
                ListadoPersonasNombreDepartamento.Add(new clsPersonDepartmentName(item));
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
                TxtBlckOperacionExitosa = "";
                //O asi
                //txtBlckOperacionExitosa = "";
                //NotifyPropertyChanged("TxtBlckOperacionExitosa");
                timer.Stop();
            });
        }
        #endregion
    }
}
