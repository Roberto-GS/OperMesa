using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Factura
    {
        #region CONSTANTES
        private const byte MIN_NUMERO = 1;
        private const byte MAX_NUMERO = 30;
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        string _numerofactura;
        int _pedidoid;
        float _subtotal;
        float _totaliva;
        float _total;
        string _estado;
        DateTime _fechaemision;
        int _usuarioid;
        #endregion

        #region CONSTRUCTORES
        public Factura(int id, string numerofactura, int pedidoid, float subtotal, float totaliva, float total, string estado, DateTime fechaemision, int usuarioid)
        {
            Id = id;
            NumeroFactura = numerofactura;
            PedidoId = pedidoid;
            Subtotal = subtotal;
            TotalIva = totaliva;
            Total = total;
            Estado = estado;
            FechaEmision = fechaemision;
            UsuarioId = usuarioid;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string NumeroFactura
        {
            get { return _numerofactura; }
            set
            {
                ValidarNumeroFactura(value);
                _numerofactura = value;
            }
        }

        public int PedidoId
        {
            get { return _pedidoid; }
            set { _pedidoid = value; }
        }

        public float Subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }

        public float TotalIva
        {
            get { return _totaliva; }
            set { _totaliva = value; }
        }

        public float Total
        {
            get { return _total; }
            set { _total = value; }
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

        public DateTime FechaEmision
        {
            get { return _fechaemision; }
            set { _fechaemision = value; }
        }

        public int UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNumeroFactura(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El número de factura no puede ser nulo o vacío");

            if (cadena.Length < MIN_NUMERO || cadena.Length > MAX_NUMERO)
                throw new Exception("El número de factura debe tener entre 1 y 30 caracteres");
        }

        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Emitida" && cadena != "Anulada")
                throw new Exception("Estado de factura inválido");
        }
        #endregion
    }
}

