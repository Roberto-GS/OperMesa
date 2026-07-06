using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PedidoLinea
    {

        #region CONSTANTES
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        private const byte MIN_OBSERVACIONES = 1;
        private const byte MAX_OBSERVACIONES = 200;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        int _pedidoid;
        int _productoid;
        int _cantidad;
        float _preciounitario;
        float _ivaporcentaje;
        string _estado;
        string _observacioneslinea;
        int _prioridad;
        DateTime? _fechaenviococina;
        DateTime? _fechainicioprep;
        DateTime? _fechalisto;
        DateTime? _fechaentregado;
        #endregion

        #region CONSTRUCTORES
        public PedidoLinea(int id, int pedidoid, int productoid, int cantidad, float preciounitario, float ivaporcentaje, string estado, string observacioneslinea, int prioridad, DateTime? fechaenviococina, DateTime? fechainicioprep, DateTime? fechalisto, DateTime? fechaentregado)
        {
            Id = id;
            PedidoId = pedidoid;
            ProductoId = productoid;
            Cantidad = cantidad;
            PrecioUnitario = preciounitario;
            IvaPorcentaje = ivaporcentaje;
            Estado = estado;
            ObservacionesLinea = observacioneslinea;
            Prioridad = prioridad;
            FechaEnvioCocina = fechaenviococina;
            FechaInicioPrep = fechainicioprep;
            FechaListo = fechalisto;
            FechaEntregado = fechaentregado;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int PedidoId
        {
            get { return _pedidoid; }
            set { _pedidoid = value; }
        }

        public int ProductoId
        {
            get { return _productoid; }
            set { _productoid = value; }
        }

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public float PrecioUnitario
        {
            get { return _preciounitario; }
            set { _preciounitario = value; }
        }

        public float IvaPorcentaje
        {
            get { return _ivaporcentaje; }
            set { _ivaporcentaje = value; }
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

        public string ObservacionesLinea
        {
            get { return _observacioneslinea; }
            set
            {
                ValidarObservacionesLinea(value);
                _observacioneslinea = value;
            }
        }

        public int Prioridad
        {
            get { return _prioridad; }
            set { _prioridad = value; }
        }

        public DateTime? FechaEnvioCocina
        {
            get { return _fechaenviococina; }
            set { _fechaenviococina = value; }
        }

        public DateTime? FechaInicioPrep
        {
            get { return _fechainicioprep; }
            set { _fechainicioprep = value; }
        }

        public DateTime? FechaListo
        {
            get { return _fechalisto; }
            set { _fechalisto = value; }
        }

        public DateTime? FechaEntregado
        {
            get { return _fechaentregado; }
            set { _fechaentregado = value; }
        }
        #endregion

        #region METODOS
        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Pendiente" && cadena != "EnPreparacion" && cadena != "Listo" && cadena != "Entregado" && cadena != "Cancelado")
                throw new Exception("Estado de línea inválido");
        }

        private void ValidarObservacionesLinea(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_OBSERVACIONES || cadena.Length > MAX_OBSERVACIONES))
                throw new Exception("Las observaciones de línea deben tener entre 1 y 200 caracteres");
        }
        #endregion

    }
}
