using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Rol
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 50;
        private const byte MIN_DESCRIPCION = 1;
        private const byte MAX_DESCRIPCION = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        string _descripcion;
        DateTime _fechacreacion;
        #endregion

        #region CONSTRUCTORES
        public Rol(int id, string nombre, string descripcion, DateTime fechacreacion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaCreacion = fechacreacion;
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

        public DateTime FechaCreacion
        {
            get { return _fechacreacion; }
            set { _fechacreacion = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNombre(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El nombre no puede ser nulo o vacío");

            if (cadena.Length < MIN_NOMBRE || cadena.Length > MAX_NOMBRE)
                throw new Exception("El nombre debe tener entre 1 y 50 caracteres");
        }

        private void ValidarDescripcion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DESCRIPCION || cadena.Length > MAX_DESCRIPCION))
                throw new Exception("La descripción debe tener entre 1 y 200 caracteres");
        }
        #endregion

    }
}
