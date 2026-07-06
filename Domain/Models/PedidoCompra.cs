using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PedidoCompra
    {
        #region CONSTANTES
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        int _proveedorid;
        string _estado;
        DateTime _fechapedido;
        DateTime? _fecharecepcion;
        float _total;
        int? _usuarioid;
        #endregion

        #region CONSTRUCTORES
        public PedidoCompra(int id, int proveedorid, string estado, DateTime fechapedido, DateTime? fecharecepcion, float total, int? usuarioid)
        {
            Id = id;
            ProveedorId = proveedorid;
            Estado = estado;
            FechaPedido = fechapedido;
            FechaRecepcion = fecharecepcion;
            Total = total;
            UsuarioId = usuarioid;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ProveedorId
        {
            get { return _proveedorid; }
            set { _proveedorid = value; }
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

        public DateTime FechaPedido
        {
            get { return _fechapedido; }
            set { _fechapedido = value; }
        }

        public DateTime? FechaRecepcion
        {
            get { return _fecharecepcion; }
            set { _fecharecepcion = value; }
        }

        public float Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public int? UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
        }
        #endregion

        #region METODOS
        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Pendiente" && cadena != "Recibido" && cadena != "Cancelado")
                throw new Exception("Estado de pedido de compra inválido");
        }
        #endregion
    }
}
