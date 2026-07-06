using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Cliente
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 100;
        private const byte MIN_APELLIDOS = 1;
        private const byte MAX_APELLIDOS = 150;
        private const byte MIN_TELEFONO = 1;
        private const byte MAX_TELEFONO = 20;
        private const byte MIN_EMAIL = 1;
        private const byte MAX_EMAIL = 150;
        private const byte MIN_OBSERVACIONES = 1;
        private const int MAX_OBSERVACIONES = 300;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        string _apellidos;
        string _telefono;
        string _email;
        string _observaciones;
        bool _esvip;
        DateTime _fechaalta;
        #endregion

        #region CONSTRUCTORES
        public Cliente(int id, string nombre, string apellidos, string telefono, string email, string observaciones, bool esvip, DateTime fechaalta)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Email = email;
            Observaciones = observaciones;
            EsVIP = esvip;
            FechaAlta = fechaalta;
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

        public string Apellidos
        {
            get { return _apellidos; }
            set
            {
                ValidarApellidos(value);
                _apellidos = value;
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

        public string Observaciones
        {
            get { return _observaciones; }
            set
            {
                ValidarObservaciones(value);
                _observaciones = value;
            }
        }

        public bool EsVIP
        {
            get { return _esvip; }
            set { _esvip = value; }
        }

        public DateTime FechaAlta
        {
            get { return _fechaalta; }
            set { _fechaalta = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNombre(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El nombre no puede ser nulo o vacío");

            if (cadena.Length < MIN_NOMBRE || cadena.Length > MAX_NOMBRE)
                throw new Exception("El nombre debe tener entre 1 y 100 caracteres");
        }

        private void ValidarApellidos(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_APELLIDOS || cadena.Length > MAX_APELLIDOS))
                throw new Exception("Los apellidos deben tener entre 1 y 150 caracteres");
        }

        private void ValidarTelefono(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El teléfono no puede ser nulo o vacío");

            if (cadena.Length < MIN_TELEFONO || cadena.Length > MAX_TELEFONO)
                throw new Exception("El teléfono debe tener entre 1 y 20 caracteres");
        }

        private void ValidarEmail(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_EMAIL || cadena.Length > MAX_EMAIL))
                throw new Exception("El email debe tener entre 1 y 150 caracteres");
        }

        private void ValidarObservaciones(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_OBSERVACIONES || cadena.Length > MAX_OBSERVACIONES))
                throw new Exception("Las observaciones deben tener entre 1 y 300 caracteres");
        }
        #endregion

    }
}
