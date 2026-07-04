using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FacturaPagos
    {

        #region MIEMBROS PRIVADOS
        int _id;
        int _facturaid;
        int _metodopagoid;
        float _importe;
        #endregion

        #region CONSTRUCTORES
        public FacturaPagos(int id, int facturaid, int metodopagoid, float importe)
        {
            Id = id;
            FacturaId = facturaid;
            MetodoPagoId = metodopagoid;
            Importe = importe;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int FacturaId
        {
            get { return _facturaid; }
            set { _facturaid = value; }
        }

        public int MetodoPagoId
        {
            get { return _metodopagoid; }
            set { _metodopagoid = value; }
        }

        public float Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }
        #endregion

    }
}
