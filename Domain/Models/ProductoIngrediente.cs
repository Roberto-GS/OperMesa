using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProductoIngrediente
    {

        #region MIEMBROS PRIVADOS
        int _productoid;
        int _ingredienteid;
        float _cantidad;
        #endregion

        #region CONSTRUCTORES
        public ProductoIngrediente(int productoid, int ingredienteid, float cantidad)
        {
            ProductoId = productoid;
            IngredienteId = ingredienteid;
            Cantidad = cantidad;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int ProductoId
        {
            get { return _productoid; }
            set { _productoid = value; }
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
        #endregion
    }
}
