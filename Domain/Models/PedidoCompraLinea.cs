using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PedidoCompraLinea
    {

        #region MIEMBROS PRIVADOS
        int _id;
        int _pedidocompraid;
        int _ingredienteid;
        float _cantidad;
        float _costeunitario;
        #endregion

        #region CONSTRUCTORES
        public PedidoCompraLinea(int id, int pedidocompraid, int ingredienteid, float cantidad, float costeunitario)
        {
            Id = id;
            PedidoCompraId = pedidocompraid;
            IngredienteId = ingredienteid;
            Cantidad = cantidad;
            CosteUnitario = costeunitario;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int PedidoCompraId
        {
            get { return _pedidocompraid; }
            set { _pedidocompraid = value; }
        }

        public int IngredienteId
        {
            get { return _ingredienteid; }
            set { _ingredienteid = value; }
        }

        public float Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public float CosteUnitario
        {
            get { return _costeunitario; }
            set { _costeunitario = value; }
        }
        #endregion

    }
}
