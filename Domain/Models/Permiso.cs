using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Permiso
    {

        #region CONSTANTES
        private const byte MIN_CODIGO = 1;
        private const byte MAX_CODIGO = 100;
        private const byte MIN_DESCRIPCION = 1;
        private const byte MAX_DESCRIPCION = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _codigo;
        string _descripcion;
        #endregion

        #region CONSTRUCTORES
        public Permiso(int id, string codigo, string descripcion)
        {
            Id = id;
            Codigo = codigo;
            Descripcion = descripcion;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                ValidarCodigo(value);
                _codigo = value;
            }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                ValidarDescripcion(value);
                _descripcion = value;
            }
        }
        #endregion

        #region METODOS
        private void ValidarCodigo(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El código no puede ser nulo o vacío");

            if (cadena.Length < MIN_CODIGO || cadena.Length > MAX_CODIGO)
                throw new Exception("El código debe tener entre 1 y 100 caracteres");
        }

        private void ValidarDescripcion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DESCRIPCION || cadena.Length > MAX_DESCRIPCION))
                throw new Exception("La descripción debe tener entre 1 y 200 caracteres");
        }
        #endregion

    }
}
