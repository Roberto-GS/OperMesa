using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Empleado
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 100;
        private const byte MIN_APELLIDOS = 1;
        private const byte MAX_APELLIDOS = 150;
        private const byte MIN_TELEFONO = 1;
        private const byte MAX_TELEFONO = 20;
        private const byte MIN_DIRECCION = 1;
        private const byte MAX_DIRECCION = 250;
        private const byte MIN_FOTO = 1;
        private const int MAX_FOTO = 300;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        int _usuarioid;
        string _nombre;
        string _apellidos;
        string _telefono;
        string _direccion;
        float? _salario;
        DateTime? _fechacontratacion;
        string _fotourl;
        bool _activo;
        #endregion

        #region CONSTRUCTORES
        public Empleado(int id, int usuarioid, string nombre, string apellidos, string telefono, string direccion, float? salario, DateTime? fechacontratacion, string fotourl, bool activo)
        {
            Id = id;
            UsuarioId = usuarioid;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Direccion = direccion;
            Salario = salario;
            FechaContratacion = fechacontratacion;
            FotoUrl = fotourl;
            Activo = activo;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
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

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                ValidarDireccion(value);
                _direccion = value;
            }
        }

        public float? Salario
        {
            get { return _salario; }
            set { _salario = value; }
        }

        public DateTime? FechaContratacion
        {
            get { return _fechacontratacion; }
            set { _fechacontratacion = value; }
        }

        public string FotoUrl
        {
            get { return _fotourl; }
            set
            {
                ValidarFotoUrl(value);
                _fotourl = value;
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
                throw new Exception("El nombre debe tener entre 1 y 100 caracteres");
        }

        private void ValidarApellidos(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_APELLIDOS || cadena.Length > MAX_APELLIDOS))
                throw new Exception("Los apellidos deben tener entre 1 y 150 caracteres");
        }

        private void ValidarTelefono(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_TELEFONO || cadena.Length > MAX_TELEFONO))
                throw new Exception("El teléfono debe tener entre 1 y 200 caracteres");
        }

        private void ValidarDireccion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DIRECCION || cadena.Length > MAX_DIRECCION))
                throw new Exception("La dirección debe tener entre 1 y 250 caracteres");
        }

        private void ValidarFotoUrl(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_FOTO || cadena.Length > MAX_FOTO))
                throw new Exception("La URL de la foto debe tener entre 1 y 300 caracteres");
        }
        #endregion

    }
}
