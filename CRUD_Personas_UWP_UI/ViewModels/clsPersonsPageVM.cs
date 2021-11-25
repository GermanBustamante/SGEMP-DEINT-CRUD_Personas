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
        //Mi vista necesita una lista de personas y la persona seleccionada(ClsPersonaNombreDepartamento), 

        #region atributos
        private clsPersonDepartmentName oPersonaSeleccionadaNombreDepartamento;
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

        public ObservableCollection<clsPersonDepartmentName> ListadoPersonasNombreDepartamento { get; set; }
        public ObservableCollection<String> ListadoNombresDepartamentos { get; set; }
        public clsPersonDepartmentName OPersonaSeleccionadaNombreDepartamento
        {
            get { return oPersonaSeleccionadaNombreDepartamento; }
            set
            {
                oPersonaSeleccionadaNombreDepartamento = value;
                //ESTE IF-ELSE ES PQ SI BORRO UNA PERSONA, LUEGO OPERSONASELECCIONADA SE PONE A NULL Y DEBO
                //CONTROLAR QUE SI ESO OCURRE EL NOMBRE DEPARTAMENTO TAMBIÉN SE PONGA NULO PARA QUE AL BORRAR NO
                //SE QUEDE NINGÚN DEPARTAMENTO SELECCIONADO
                //oPersonaSeleccionadaNombreDepartamento.IdDepartamento = oPersonaSeleccionadaNombreDepartamento.IdDepartamento != null ? ClsListadoDepartamentosBL.getNombreDepartamentoBL(oPersonaSeleccionadaNombreDepartamento.IdDepartamento) : null;

                NotifyPropertyChanged("OPersonaSeleccionada");
                editPersonCommand.RaiseCanExecuteChanged();
                deletePersonCommand.RaiseCanExecuteChanged();
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
            return oPersonaSeleccionadaNombreDepartamento == null;
        }
        private void DeletePersonCommand_Execute()
        {
            //¿DUDA QUE HAGO CON EL INT QUE DEVUELVE? ESTA BIEN ASÍ
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
