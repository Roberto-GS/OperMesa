using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CategoriaProducto
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 80;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        int _orden;
        bool _activa;
        #endregion

        #region CONSTRUCTORES
        public CategoriaProducto(int id, string nombre, int orden, bool activa)
        {
            Id = id;
            Nombre = nombre;
            Orden = orden;
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

        public int Orden
        {
            get { return _orden; }
            set { _orden = value; }
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
        #endregion

    }
}
