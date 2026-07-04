using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LogsAuditoria
    {

        #region CONSTANTES
        private const byte MIN_ENTIDAD = 1;
        private const byte MAX_ENTIDAD = 80;
        private const byte MIN_ENTIDADID = 1;
        private const byte MAX_ENTIDADID = 40;
        private const byte MIN_ACCION = 1;
        private const byte MAX_ACCION = 20;
        private const byte MIN_IP = 1;
        private const byte MAX_IP = 45;
        private const byte MIN_USERAGENT = 1;
        private const int MAX_USERAGENT = 300;
        #endregion

        #region MIEMBROS PRIVADOS
        long _id;
        int? _usuarioid;
        string _entidad;
        string _entidadid;
        string _accion;
        string _datosanteriores; // NVARCHAR(MAX) sin validación estricta de tope
        string _datosnuevos;     // NVARCHAR(MAX) sin validación estricta de tope
        string _ip;
        string _useragent;
        DateTime _fechaaccion;
        #endregion

        #region CONSTRUCTORES
        public LogsAuditoria(long id, int? usuarioid, string entidad, string entidadid, string accion, string datosanteriores, string datosnuevos, string ip, string useragent, DateTime fechaaccion)
        {
            Id = id;
            UsuarioId = usuarioid;
            Entidad = entidad;
            EntidadId = entidadid;
            Accion = accion;
            DatosAnteriores = datosanteriores;
            DatosNuevos = datosnuevos;
            IP = ip;
            UserAgent = useragent;
            FechaAccion = fechaaccion;
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

        public string Entidad
        {
            get { return _entidad; }
            set
            {
                ValidarEntidad(value);
                _entidad = value;
            }
        }

        public string EntidadId
        {
            get { return _entidadid; }
            set
            {
                ValidarEntidadId(value);
                _entidadid = value;
            }
        }

        public string Accion
        {
            get { return _accion; }
            set
            {
                ValidarAccion(value);
                _accion = value;
            }
        }

        public string DatosAnteriores
        {
            get { return _datosanteriores; }
            set { _datosanteriores = value; }
        }

        public string DatosNuevos
        {
            get { return _datosnuevos; }
            set { _datosnuevos = value; }
        }

        public string IP
        {
            get { return _ip; }
            set
            {
                ValidarIP(value);
                _ip = value;
            }
        }

        public string UserAgent
        {
            get { return _useragent; }
            set
            {
                ValidarUserAgent(value);
                _useragent = value;
            }
        }

        public DateTime FechaAccion
        {
            get { return _fechaaccion; }
            set { _fechaaccion = value; }
        }
        #endregion

        #region METODOS
        private void ValidarEntidad(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("La entidad no puede ser nula o vacía");

            if (cadena.Length < MIN_ENTIDAD || cadena.Length > MAX_ENTIDAD)
                throw new Exception("La entidad debe tener entre 1 y 80 caracteres");
        }

        private void ValidarEntidadId(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_ENTIDADID || cadena.Length > MAX_ENTIDADID))
                throw new Exception("El ID de la entidad debe tener entre 1 y 40 caracteres");
        }

        private void ValidarAccion(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("La acción no puede ser nula o vacía");

            if (cadena.Length < MIN_ACCION || cadena.Length > MAX_ACCION)
                throw new Exception("La acción debe tener entre 1 y 20 caracteres");

            if (cadena != "Create" && cadena != "Update" && cadena != "Delete")
                throw new Exception("Acción de auditoría no permitida");
        }

        private void ValidarIP(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_IP || cadena.Length > MAX_IP))
                throw new Exception("La IP debe tener entre 1 y 45 caracteres");
        }

        private void ValidarUserAgent(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_USERAGENT || cadena.Length > MAX_USERAGENT))
                throw new Exception("El User Agent debe tener entre 1 y 300 caracteres");
        }
        #endregion

    }
}
