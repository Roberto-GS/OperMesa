using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovimientosCaja
    {

        #region CONSTANTES
        private const byte MIN_TIPO = 1;
        private const byte MAX_TIPO = 20;
        private const byte MIN_DESCRIPCION = 1;
        private const byte MAX_DESCRIPCION = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        long _id;
        int _cajadiariaid;
        string _tipo;
        float _importe;
        string _descripcion;
        int? _facturaid;
        int? _usuarioid;
        DateTime _fechamovimiento;
        #endregion

        #region CONSTRUCTORES
        public MovimientosCaja(long id, int cajadiariaid, string tipo, float importe, string descripcion, int? facturaid, int? usuarioid, DateTime fechamovimiento)
        {
            Id = id;
            CajaDiariaId = cajadiariaid;
            Tipo = tipo;
            Importe = importe;
            Descripcion = descripcion;
            FacturaId = facturaid;
            UsuarioId = usuarioid;
            FechaMovimiento = fechamovimiento;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CajaDiariaId
        {
            get { return _cajadiariaid; }
            set { _cajadiariaid = value; }
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

        public float Importe
        {
            get { return _importe; }
            set { _importe = value; }
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

        public int? FacturaId
        {
            get { return _facturaid; }
            set { _facturaid = value; }
        }

        public int? UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
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

            if (cadena != "Apertura" && cadena != "Ingreso" && cadena != "Retirada" && cadena != "Correccion" && cadena != "Cierre")
                throw new Exception("Tipo de movimiento de caja inválido");
        }

        private void ValidarDescripcion(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_DESCRIPCION || cadena.Length > MAX_DESCRIPCION))
                throw new Exception("La descripción debe tener entre 1 y 200 caracteres");
        }
        #endregion

    }
}
