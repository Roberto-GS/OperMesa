using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Mesa
    {

        #region CONSTANTES
        private const byte MIN_NUMERO = 1;
        private const byte MAX_NUMERO = 10;
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _numero;
        int _zonaid;
        int _capacidad;
        string _estado;
        int? _posicionx;
        int? _posiciony;
        bool _activa;
        #endregion

        #region CONSTRUCTORES
        public Mesa(int id, string numero, int zonaid, int capacidad, string estado, int? posicionx, int? posiciony, bool activa)
        {
            Id = id;
            Numero = numero;
            ZonaId = zonaid;
            Capacidad = capacidad;
            Estado = estado;
            PosicionX = posicionx;
            PosicionY = posiciony;
            Activa = activa;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set
            {
                ValidarNumero(value);
                _numero = value;
            }
        }

        public int ZonaId
        {
            get { return _zonaid; }
            set { _zonaid = value; }
        }

        public int Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                ValidarEstado(value);
                _estado = value;
            }
        }

        public int? PosicionX
        {
            get { return _posicionx; }
            set { _posicionx = value; }
        }

        public int? PosicionY
        {
            get { return _posiciony; }
            set { _posiciony = value; }
        }

        public bool Activa
        {
            get { return _activa; }
            set { _activa = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNumero(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El número no puede ser nulo o vacío");

            if (cadena.Length < MIN_NUMERO || cadena.Length > MAX_NUMERO)
                throw new Exception("El número debe tener entre 1 y 10 caracteres");
        }

        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Libre" && cadena != "Ocupada" && cadena != "Reservada" && cadena != "FueraDeServicio")
                throw new Exception("El estado ingresado no es válido.");
        }
        #endregion

    }
}
