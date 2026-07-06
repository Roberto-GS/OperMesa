using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Notificacion
    {

        #region CONSTANTES
        private const byte MIN_TIPO = 1;
        private const byte MAX_TIPO = 40;
        private const byte MIN_TITULO = 1;
        private const byte MAX_TITULO = 150;
        private const byte MIN_MENSAJE = 1;
        private const int MAX_MENSAJE = 400;
        #endregion

        #region MIEMBROS PRIVADOS
        long _id;
        int? _usuarioid;
        string _tipo;
        string _titulo;
        string _mensaje;
        bool _leida;
        DateTime _fechacreacion;
        #endregion

        #region CONSTRUCTORES
        public Notificacion(long id, int? usuarioid, string tipo, string titulo, string mensaje, bool leida, DateTime fechacreacion)
        {
            Id = id;
            UsuarioId = usuarioid;
            Tipo = tipo;
            Titulo = titulo;
            Mensaje = mensaje;
            Leida = leida;
            FechaCreacion = fechacreacion;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set
            {
                ValidarTipo(value);
                _tipo = value;
            }
        }

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                ValidarTitulo(value);
                _titulo = value;
            }
        }

        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                ValidarMensaje(value);
                _mensaje = value;
            }
        }

        public bool Leida
        {
            get { return _leida; }
            set { _leida = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _fechacreacion; }
            set { _fechacreacion = value; }
        }
        #endregion

        #region METODOS
        private void ValidarTipo(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El tipo no puede ser nulo o vacío");

            if (cadena.Length < MIN_TIPO || cadena.Length > MAX_TIPO)
                throw new Exception("El tipo debe tener entre 1 y 40 caracteres");
        }

        private void ValidarTitulo(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El título no puede ser nulo o vacío");

            if (cadena.Length < MIN_TITULO || cadena.Length > MAX_TITULO)
                throw new Exception("El título debe tener entre 1 y 150 caracteres");
        }

        private void ValidarMensaje(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El mensaje no puede ser nulo o vacío");

            if (cadena.Length < MIN_MENSAJE || cadena.Length > MAX_MENSAJE)
                throw new Exception("El mensaje debe tener entre 1 y 400 caracteres");
        }
        #endregion

    }
}
