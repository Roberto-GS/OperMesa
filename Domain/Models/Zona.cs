using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Zona
    {
        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 80;
        private const byte MIN_DESCRIPCION = 1;
        private const byte MAX_DESCRIPCION = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        string _descripcion;
        bool _activa;
        #endregion

        #region CONSTRUCTORES
        public Zona(int id, string nombre, string descripcion, bool activa)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Activa = activa;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                ValidarNombre(value);
                _nombre = value;
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

        public bool Activa
        {
            get { return _activa; }
            set { _activa = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNombre(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El nombre no puede ser nulo o vacío");

            if (cadena.Length < MIN_NOMBRE || cadena.Length > MAX_NOMBRE)
                throw new Exception("El nombre debe tener entre 1 y 80 caracteres");
        }

        private void ValidarDescripcion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DESCRIPCION || cadena.Length > MAX_DESCRIPCION))
                throw new Exception("La descripción debe tener entre 1 y 200 caracteres");
        }
        #endregion
    }
}
