using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Producto
    {

        #region CONSTANTES
        private const byte MIN_NOMBRE = 1;
        private const byte MAX_NOMBRE = 150;
        private const byte MIN_DESCRIPCION = 1;
        private const int MAX_DESCRIPCION = 400;
        private const byte MIN_IMAGEN = 1;
        private const int MAX_IMAGEN = 300;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _nombre;
        string _descripcion;
        int _categoriaid;
        float _precio;
        float _ivaporcentaje;
        string _imagenurl;
        bool _disponible;
        int? _tiempopreparacionmin;
        DateTime _fechacreacion;
        #endregion

        #region CONSTRUCTORES
        public Producto(int id, string nombre, string descripcion, int categoriaid, float precio, float ivaporcentaje, string imagenurl, bool disponible, int? tiempopreparacionmin, DateTime fechacreacion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            CategoriaId = categoriaid;
            Precio = precio;
            IvaPorcentaje = ivaporcentaje;
            ImagenUrl = imagenurl;
            Disponible = disponible;
            TiempoPreparacionMin = tiempopreparacionmin;
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

        public int CategoriaId
        {
            get { return _categoriaid; }
            set { _categoriaid = value; }
        }

        public float Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        public float IvaPorcentaje
        {
            get { return _ivaporcentaje; }
            set { _ivaporcentaje = value; }
        }

        public string ImagenUrl
        {
            get { return _imagenurl; }
            set
            {
                ValidarImagenUrl(value);
                _imagenurl = value;
            }
        }

        public bool Disponible
        {
            get { return _disponible; }
            set { _disponible = value; }
        }

        public int? TiempoPreparacionMin
        {
            get { return _tiempopreparacionmin; }
            set { _tiempopreparacionmin = value; }
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
                throw new Exception("El nombre debe tener entre 1 y 150 caracteres");
        }

        private void ValidarDescripcion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DESCRIPCION || cadena.Length > MAX_DESCRIPCION))
                throw new Exception("La descripción debe tener entre 1 y 400 caracteres");
        }

        private void ValidarImagenUrl(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_IMAGEN || cadena.Length > MAX_IMAGEN))
                throw new Exception("La URL de la imagen debe tener entre 1 y 300 caracteres");
        }
        #endregion

    }
}
