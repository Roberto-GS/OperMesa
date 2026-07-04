using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum EstadoLineaPedido : byte
    {
        Pendiente, 
        EnPreparacion, 
        Listo, 
        Entregado, 
        Cancelado
    }
}
