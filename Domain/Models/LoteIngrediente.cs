using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class LoteIngrediente
    {

        #region CONSTANTES
        private const byte MIN_NUMEROLOTE = 1;
        private const byte MAX_NUMEROLOTE = 50;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        int _ingredienteid;
        int? _pedidocompraid;
        string _numerolote;
        float _cantidad;
        float _cantidadrestante;
        float _costeunitario;
        DateTime _fechaentrada;
        DateTime? _fechadacaducidad;
        #endregion

        #region CONSTRUCTORES
        public LoteIngrediente(int id, int ingredienteid, int? pedidocompraid, string numerolote, float cantidad, float cantidadrestante, float costeunitario, DateTime fechaentrada, DateTime? fechacaducidad)
        {
            Id = id;
            IngredienteId = ingredienteid;
            PedidoCompraId = pedidocompraid;
            NumeroLote = numerolote;
            Cantidad = cantidad;
            CantidadRestante = cantidadrestante;
            CosteUnitario = costeunitario;
            FechaEntrada = fechaentrada;
            FechaCaducidad = fechacaducidad;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int IngredienteId
        {
            get { return _ingredienteid; }
            set { _ingredienteid = value; }
        }

        public int? PedidoCompraId
        {
            get { return _pedidocompraid; }
            set { _pedidocompraid = value; }
        }

        public string NumeroLote
        {
            get { return _numerolote; }
            set
            {
                ValidarNumeroLote(value);
                _numerolote = value;
            }
        }

        public float Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public float CantidadRestante
        {
            get { return _cantidadrestante; }
            set
            {
                ValidarCantidadRestante(value);
                
                _cantidadrestante = value;
            }
        }

      

        public float CosteUnitario
        {
            get { return _costeunitario; }
            set { _costeunitario = value; }
        }

        public DateTime FechaEntrada
        {
            get { return _fechaentrada; }
            set { _fechaentrada = value; }
        }

        public DateTime? FechaCaducidad
        {
            get { return _fechadacaducidad; }
            set { _fechadacaducidad = value; }
        }
        #endregion

        #region METODOS
        private void ValidarNumeroLote(string cadena)
        {
            if (cadena != null && (cadena.Length < MIN_NUMEROLOTE || cadena.Length > MAX_NUMEROLOTE))
                throw new Exception("El número de lote debe tener entre 1 y 50 caracteres");
        }

        private void ValidarCantidadRestante(float cadena)
        {
            if (cadena < 0 || cadena > _cantidad)
                throw new Exception("La cantidad restante debe ser coherente con la inicial y no negativa");
        }
        #endregion

    }
}
