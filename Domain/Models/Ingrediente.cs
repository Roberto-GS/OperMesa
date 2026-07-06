using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Ingrediente
    {
        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 120;
        private const byte MIN_UNIDAD = 1;
        private const byte MAX_UNIDAD = 20;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        string _unidadmedida;
        float _stockactual;
        float _stockminimo;
        int? _proveedorid;
        #endregion

        #region CONSTRUCTORES
        public Ingrediente(int id, string nombre, string unidadmedida, float stockactual, float stockminimo, int? proveedorid)
        {
            Id = id;
            Nombre = nombre;
            UnidadMedida = unidadmedida;
            StockActual = stockactual;
            StockMinimo = stockminimo;
            ProveedorId = proveedorid;
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

        public string UnidadMedida
        {
            get { return _unidadmedida; }
            set
            {
                ValidarUnidadMedida(value);
                _unidadmedida = value;
            }
        }

        public float StockActual
        {
            get { return _stockactual; }
            set
            {
                if (value < 0) throw new Exception("El Stock Actual no puede ser negativo");
                _stockactual = value;
            }
        }

        public float StockMinimo
        {
            get { return _stockminimo; }
            set { _stockminimo = value; }
        }

        public int? ProveedorId
        {
            get { return _proveedorid; }
            set { _proveedorid = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNombre(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El nombre no puede ser nulo o vacío");

            if (cadena.Length < MIN_NOMBRE || cadena.Length > MAX_NOMBRE)
                throw new Exception("El nombre debe tener entre 1 y 120 caracteres");
        }

        private void ValidarUnidadMedida(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("La unidad de medida no puede ser nula o vacía");

            if (cadena.Length < MIN_UNIDAD || cadena.Length > MAX_UNIDAD)
                throw new Exception("La unidad de medida debe tener entre 1 y 20 caracteres");
        }
        #endregion
    }
}
