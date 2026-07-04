using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Reserva
    {

        #region CONSTANTES
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        private const byte MIN_OBSERVACIONES = 1;
        private const int MAX_OBSERVACIONES = 300;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        int _clienteid;
        int _numeropersonas;
        DateTime _fechahora;
        int _duracionminutos;
        int? _mesaid;
        string _estado;
        string _observaciones;
        int? _usuariocreaid;
        DateTime _fechacreacion;
        #endregion

        #region CONSTRUCTORES
        public Reserva(int id, int clienteid, int numeropersonas, DateTime fechahora, int duracionminutos, int? mesaid, string estado, string observaciones, int? usuariocreaid, DateTime fechacreacion)
        {
            Id = id;
            ClienteId = clienteid;
            NumeroPersonas = numeropersonas;
            FechaHora = fechahora;
            DuracionMinutos = duracionminutos;
            MesaId = mesaid;
            Estado = estado;
            Observaciones = observaciones;
            UsuarioCreaId = usuariocreaid;
            FechaCreacion = fechacreacion;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ClienteId
        {
            get { return _clienteid; }
            set { _clienteid = value; }
        }

        public int NumeroPersonas
        {
            get { return _numeropersonas; }
            set { _numeropersonas = value; }
        }

        public DateTime FechaHora
        {
            get { return _fechahora; }
            set { _fechahora = value; }
        }

        public int DuracionMinutos
        {
            get { return _duracionminutos; }
            set { _duracionminutos = value; }
        }

        public int? MesaId
        {
            get { return _mesaid; }
            set { _mesaid = value; }
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

        public string Observaciones
        {
            get { return _observaciones; }
            set
            {
                ValidarObservaciones(value);
                _observaciones = value;
            }
        }

        public int? UsuarioCreaId
        {
            get { return _usuariocreaid; }
            set { _usuariocreaid = value; }
        }

        public DateTime FechaCreacion
        {
            get { return _fechacreacion; }
            set { _fechacreacion = value; }
        }
        #endregion

        #region METODOS
        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Pendiente" && cadena != "Confirmada" && cadena != "Cancelada" && cadena != "Completada" && cadena != "NoShow")
                throw new Exception("Estado de reserva inválido");
        }

        private void ValidarObservaciones(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_OBSERVACIONES || cadena.Length > MAX_OBSERVACIONES))
                throw new Exception("Las observaciones deben tener entre 1 y 300 caracteres");
        }
        #endregion

    }
}
