using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MetodosPago
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 50;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        #endregion

        #region CONSTRUCTORES
        public MetodosPago(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
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
        #endregion

        #region METODOS
        private void ValidarNombre(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El nombre no puede ser nulo o vacío");

            if (cadena.Length < MIN_NOMBRE || cadena.Length > MAX_NOMBRE)
                throw new Exception("El nombre debe tener entre 1 y 50 caracteres");
        }
        #endregion

    }
}
