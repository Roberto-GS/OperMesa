using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Usuario
    {
        #region CONSTANTES
        private const byte MIN_EMAIL = 1;
        private const byte MAX_EMAIL = 150;
        private const byte MIN_PASSWORD = 1;
        private const byte MAX_PASSWORD = 255;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _email;
        string _passwordhash;
        int _rolid;
        bool _activo;
        int _tokenversion;
        DateTime? _fechaultimoreset;
        DateTime _fechacreacion;
        DateTime? _ultimoacceso;
        #endregion

        #region CONSTRUCTORES
        public Usuario(int id, string email, string passwordhash, int rolid, bool activo, int tokenversion, DateTime? fechaultimoreset, DateTime fechacreacion, DateTime? ultimoacceso)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordhash;
            RolId = rolid;
            Activo = activo;
            TokenVersion = tokenversion;
            FechaUltimoReset = fechaultimoreset;
            FechaCreacion = fechacreacion;
            UltimoAcceso = ultimoacceso;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
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

        public string PasswordHash
        {
            get { return _passwordhash; }
            set
            {
                ValidarPasswordHash(value);
                _passwordhash = value;
            }
        }

        public int RolId
        {
            get { return _rolid; }
            set { _rolid = value; }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public int TokenVersion
        {
            get { return _tokenversion; }
            set { _tokenversion = value; }
        }

        public DateTime? FechaUltimoReset
        {
            get { return _fechaultimoreset; }
            set { _fechaultimoreset = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _fechacreacion; }
            set { _fechacreacion = value; }
        }

        public DateTime? UltimoAcceso
        {
            get { return _ultimoacceso; }
            set { _ultimoacceso = value; }
        }
        #endregion

        #region METODOS
        private void ValidarEmail(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El email no puede ser nulo o vacío");

            if (cadena.Length < MIN_EMAIL || cadena.Length > MAX_EMAIL)
                throw new Exception("El email debe tener entre 1 y 150 caracteres");
        }

        private void ValidarPasswordHash(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El password hash no puede ser nulo o vacío");

            if (cadena.Length < MIN_PASSWORD || cadena.Length > MAX_PASSWORD)
                throw new Exception("El password hash debe tener entre 1 y 255 caracteres");
        }
        #endregion
    }
}
