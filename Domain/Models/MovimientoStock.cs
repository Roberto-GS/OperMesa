using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovimientoStock
    {

        #region CONSTANTES
        private const byte MIN_TIPO = 1;
        private const byte MAX_TIPO = 20;
        private const byte MIN_MOTIVO = 1;
        private const byte MAX_MOTIVO = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        long _id;
        int _ingredienteid;
        int? _loteid;
        string _tipo;
        float _cantidad;
        string _motivo;
        int? _usuarioid;
        int? _pedidolineaid;
        DateTime _fechamovimiento;
        #endregion

        #region CONSTRUCTORES
        public MovimientoStock(long id, int ingredienteid, int? loteid, string tipo, float cantidad, string motivo, int? usuarioid, int? pedidolineaid, DateTime fechamovimiento)
        {
            Id = id;
            IngredienteId = ingredienteid;
            LoteId = loteid;
            Tipo = tipo;
            Cantidad = cantidad;
            Motivo = motivo;
            UsuarioId = usuarioid;
            PedidoLineaId = pedidolineaid;
            FechaMovimiento = fechamovimiento;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int IngredienteId
        {
            get { return _ingredienteid; }
            set { _ingredienteid = value; }
        }

        public int? LoteId
        {
            get { return _loteid; }
            set { _loteid = value; }
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

        public float Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public string Motivo
        {
            get { return _motivo; }
            set
            {
                ValidarMotivo(value);
                _motivo = value;
            }
        }

        public int? UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
        }

        public int? PedidoLineaId
        {
            get { return _pedidolineaid; }
            set { _pedidolineaid = value; }
        }

        public DateTime FechaMovimiento
        {
            get { return _fechamovimiento; }
            set { _fechamovimiento = value; }
        }
        #endregion

        #region METODOS
        private void ValidarTipo(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El tipo no puede ser nulo o vacío");

            if (cadena.Length < MIN_TIPO || cadena.Length > MAX_TIPO)
                throw new Exception("El tipo debe tener entre 1 y 20 caracteres");

            if (cadena != "Entrada" && cadena != "Salida" && cadena != "Ajuste" && cadena != "Merma")
                throw new Exception("Tipo de movimiento inválido");
        }

        private void ValidarMotivo(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_MOTIVO || cadena.Length > MAX_MOTIVO))
                throw new Exception("El motivo debe tener entre 1 y 200 caracteres");
        }
        #endregion

    }
}
