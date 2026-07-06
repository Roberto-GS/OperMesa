using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Configuracion
    {
        #region CONSTANTES
        private const byte MIN_CLAVE = 1;
        private const byte MAX_CLAVE = 80;
        private const byte MIN_VALOR = 1;
        private const int MAX_VALOR = 500;
        private const byte MIN_DESCRIPCION = 1;
        private const byte MAX_DESCRIPCION = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _clave;
        string _valor;
        string _descripcion;
        DateTime _fechamodificacion;
        #endregion

        #region CONSTRUCTORES
        public Configuracion(int id, string clave, string valor, string descripcion, DateTime fechamodificacion)
        {
            Id = id;
            Clave = clave;
            Valor = valor;
            Descripcion = descripcion;
            FechaModificacion = fechamodificacion;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Clave
        {
            get { return _clave; }
            set
            {
                ValidarClave(value);
                _clave = value;
            }
        }

        public string Valor
        {
            get { return _valor; }
            set
            {
                ValidarValor(value);
                _valor = value;
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

        public DateTime FechaModificacion
        {
            get { return _fechamodificacion; }
            set { _fechamodificacion = value; }
        }
        #endregion

        #region METODOS
        private void ValidarClave(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("La clave no puede ser nula o vacía");

            if (cadena.Length < MIN_CLAVE || cadena.Length > MAX_CLAVE)
                throw new Exception("La clave debe tener entre 1 y 80 caracteres");
        }

        private void ValidarValor(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El valor no puede ser nulo o vacío");

            if (cadena.Length < MIN_VALOR || cadena.Length > MAX_VALOR)
                throw new Exception("El valor debe tener entre 1 y 500 caracteres");
        }

        private void ValidarDescripcion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DESCRIPCION || cadena.Length > MAX_DESCRIPCION))
                throw new Exception("La descripción debe tener entre 1 y 200 caracteres");
        }
        #endregion
    }
}
