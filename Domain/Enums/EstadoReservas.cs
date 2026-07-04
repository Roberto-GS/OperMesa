using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    // Para la clase Reserva
    public enum EstadoReservas : byte
    {
        Pendiente, 
        Confirmada, 
        Cancelada, 
        Completada
    }
}
