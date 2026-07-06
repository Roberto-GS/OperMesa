using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PedidoMesa
    {
        #region MIEMBROS PRIVADOS
        int _pedidoid;
        int _mesaid;
        #endregion

        #region CONSTRUCTORES
        public PedidoMesa(int pedidoid, int mesaid)
        {
            PedidoId = pedidoid;
            MesaId = mesaid;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int PedidoId
        {
            get { return _pedidoid; }
            set { _pedidoid = value; }
        }

        public int MesaId
        {
            get { return _mesaid; }
            set { _mesaid = value; }
        }
        #endregion
    }
}
