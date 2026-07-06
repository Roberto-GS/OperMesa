using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum TipoMovimientoCaja : byte
    {
        Ingreso, 
        Retirada, 
        Correccion, 
        Cancelar
    }
}
