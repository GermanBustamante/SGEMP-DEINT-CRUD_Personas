﻿using CRUD_Personas_DAL.Conexion;
using CRUD_Personas_Entidades;
using DAL.Utilidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace CRUD_Personas_DAL.Listados
{
    public class ClsListadoDepartamentosDAL : ClsUtililidadSelectDAL
    {
        #region constantes
        public const String INSTRUCCION_SELECT_NOMBRE_DEPARTAMENTO_PK = "SELECT nombreDepartamento FROM Departamentos WHERE IdDepartamento=";
        public const String INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS = "SELECT nombreDepartamento FROM Departamentos";
        public const String COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS = "nombreDepartamento";
        public const String COLUMNA_IDDEPARTAMENTO_TABLA_DEPARTAMENTOS = "IDDepartamento";
        public const String INTRUCCION_SELECT_ID_DEPARTAMENTO_DADO_NOMBRE = "SELECT IDDepartamento FROM Departamentos WHERE "+COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS+"=";
        public const String INSTRUCCION_SELECT_DEPARTAMENTOS = "SELECT * FROM Departamentos";

        #endregion

        #region propiedades publicas
        #endregion
        #region metodos publicos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreDepartamento"></param>
        /// <returns></returns>
        public static int getIdDepartamentoDAL(string nombreDepartamento)
        {
            int idDepartamento = 0;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INTRUCCION_SELECT_ID_DEPARTAMENTO_DADO_NOMBRE, nombreDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                idDepartamento = (int)MiLector[COLUMNA_IDDEPARTAMENTO_TABLA_DEPARTAMENTOS];
            }
            return idDepartamento;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ClsDepartamento> getListadoDepartamentosDAL()
        {
            ObservableCollection<ClsDepartamento> listadoDepartamentos = null;
            instanciarConexion();
            MiLector = ejecutarSelect(INSTRUCCION_SELECT_DEPARTAMENTOS);

            if (MiLector.HasRows)
            {
                listadoDepartamentos = rellenarListadoDepartamentos();
            }

            cerrarFlujos();

            return listadoDepartamentos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepartamento"></param>
        /// <returns></returns>
        public static String getNombreDepartamentoDAL (int idDepartamento)
        {
            String nombreDepartamento = null;
            instanciarConexion();
            MiLector = ejecutarSelectCondicion(INSTRUCCION_SELECT_NOMBRE_DEPARTAMENTO_PK, idDepartamento);

            if (MiLector.HasRows)
            {
                MiLector.Read();
                nombreDepartamento = (String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS];
            }

            cerrarFlujos();

            return nombreDepartamento;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<String> getListadoNombresDepartamentosDAL()
        {
            ObservableCollection<String> listadoNombresDepartamentos = null;
            instanciarConexion();
            MiLector = ejecutarSelect(INSTRUCCION_SELECT_NOMBRES_DEPARTAMENTOS);

            if (MiLector.HasRows)
            {
                listadoNombresDepartamentos = rellenarListadoNombresDepartamento();
            }

            cerrarFlujos();

            return listadoNombresDepartamentos;
        }
        #endregion
        #region metodos privados
        private static ObservableCollection<string> rellenarListadoNombresDepartamento()
        {
            ObservableCollection<String> listadoNombresDepartamentos = new ObservableCollection<String>();
            while (MiLector.Read())
            {
                listadoNombresDepartamentos.Add((String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS]);
            }
            return listadoNombresDepartamentos;
        }
        private static ObservableCollection<ClsDepartamento> rellenarListadoDepartamentos()
        {
            ObservableCollection<ClsDepartamento> listadoDepartamentos = new ObservableCollection<ClsDepartamento>();
            while (MiLector.Read())
            {
                int idDepartamento = (int)MiLector[COLUMNA_IDDEPARTAMENTO_TABLA_DEPARTAMENTOS];
                String nombreDepartamento = (String)MiLector[COLUMNA_NOMBRE_DEPARTAMENTO_TABLA_DEPARTAMENTOS];
                listadoDepartamentos.Add(new ClsDepartamento(idDepartamento, nombreDepartamento));
            }
            return listadoDepartamentos;
        }
        #endregion
    }



}
