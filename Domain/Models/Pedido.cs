using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Pedido
    {

        #region CONSTANTES
        private const byte MIN_TIPOPEDIDO = 1;
        private const byte MAX_TIPOPEDIDO = 20;
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        private const byte MIN_OBSERVACIONES = 1;
        private const int MAX_OBSERVACIONES = 300;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        int? _clienteid;
        string _tipopedido;
        string _estado;
        int _usuarioid;
        string _observaciones;
        DateTime _fechaapertura;
        DateTime? _fechacierre;
        #endregion

        #region CONSTRUCTORES
        public Pedido(int id, int? clienteid, string tipopedido, string estado, int usuarioid, string observaciones, DateTime fechaapertura, DateTime? fechacierre)
        {
            Id = id;
            ClienteId = clienteid;
            TipoPedido = tipopedido;
            Estado = estado;
            UsuarioId = usuarioid;
            Observaciones = observaciones;
            FechaApertura = fechaapertura;
            FechaCierre = fechacierre;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? ClienteId
        {
            get { return _clienteid; }
            set { _clienteid = value; }
        }

        public string TipoPedido
        {
            get { return _tipopedido; }
            set
            {
                ValidarTipoPedido(value);
                _tipopedido = value;
            }
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

        public int UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
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

        public DateTime FechaApertura
        {
            get { return _fechaapertura; }
            set { _fechaapertura = value; }
        }

        public DateTime? FechaCierre
        {
            get { return _fechacierre; }
            set { _fechacierre = value; }
        }
        #endregion

        #region METODOS
        private void ValidarTipoPedido(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El tipo de pedido no puede ser nulo o vacío");

            if (cadena.Length < MIN_TIPOPEDIDO || cadena.Length > MAX_TIPOPEDIDO)
                throw new Exception("El tipo de pedido debe tener entre 1 y 20 caracteres");

            if (cadena != "Local" && cadena != "ParaLlevar" && cadena != "Domicilio")
                throw new Exception("Tipo de pedido inválido");
        }

        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Abierto" && cadena != "EnCocina" && cadena != "Listo" && cadena != "Servido" && cadena != "Cerrado" && cadena != "Cancelado")
                throw new Exception("Estado de pedido inválido");
        }

        private void ValidarObservaciones(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_OBSERVACIONES || cadena.Length > MAX_OBSERVACIONES))
                throw new Exception("Las observaciones deben tener entre 1 y 300 caracteres");
        }
        #endregion

    }
}
