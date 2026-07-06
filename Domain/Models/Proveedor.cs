using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Proveedor
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 150;
        private const byte MIN_CIF = 1;
        private const byte MAX_CIF = 20;
        private const byte MIN_TELEFONO = 1;
        private const byte MAX_TELEFONO = 20;
        private const byte MIN_EMAIL = 1;
        private const byte MAX_EMAIL = 150;
        private const byte MIN_DIRECCION = 1;
        private const byte MAX_DIRECCION = 250;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        string _cif;
        string _telefono;
        string _email;
        string _direccion;
        bool _activo;
        #endregion

        #region CONSTRUCTORES
        public Proveedor(int id, string nombre, string cif, string telefono, string email, string direccion, bool activo)
        {
            Id = id;
            Nombre = nombre;
            CIF = cif;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
            Activo = activo;
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

        public string CIF
        {
            get { return _cif; }
            set
            {
                ValidarCIF(value);
                _cif = value;
            }
        }

        public string Telefono
        {
            get { return _telefono; }
            set
            {
                ValidarTelefono(value);
                _telefono = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                ValidarEmail(value);
                _email = value;
            }
        }

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                ValidarDireccion(value);
                _direccion = value;
            }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNombre(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El nombre no puede ser nulo o vacío");

            if (cadena.Length < MIN_NOMBRE || cadena.Length > MAX_NOMBRE)
                throw new Exception("El nombre debe tener entre 1 y 150 caracteres");
        }

        private void ValidarCIF(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_CIF || cadena.Length > MAX_CIF))
                throw new Exception("El CIF debe tener entre 1 y 20 caracteres");
        }

        private void ValidarTelefono(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_TELEFONO || cadena.Length > MAX_TELEFONO))
                throw new Exception("El teléfono debe tener entre 1 y 20 caracteres");
        }

        private void ValidarEmail(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_EMAIL || cadena.Length > MAX_EMAIL))
                throw new Exception("El email debe tener entre 1 y 150 caracteres");
        }

        private void ValidarDireccion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DIRECCION || cadena.Length > MAX_DIRECCION))
                throw new Exception("La dirección debe tener entre 1 y 250 caracteres");
        }
        #endregion

    }
}
