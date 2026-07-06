using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CajaDiaria
    {
        #region CONSTANTES
        private const byte MIN_ESTADO = 1;
        private const byte MAX_ESTADO = 20;
        #endregion

        #region MIEMBROS PRIVADOS
        int _id;
        DateTime _fecha; // Almacena DATE
        float _saldoinicial;
        float? _saldofinal;
        string _estado;
        int? _usuarioaperturaid;
        int? _usuariocierreid;
        DateTime _fechaapertura;
        DateTime? _fechacierre;
        #endregion

        #region CONSTRUCTORES
        public CajaDiaria(int id, DateTime fecha, float saldoinicial, float? saldofinal, string estado, int? usuarioaperturaid, int? usuariocierreid, DateTime fechaapertura, DateTime? fechacierre)
        {
            Id = id;
            Fecha = fecha;
            SaldoInicial = saldoinicial;
            SaldoFinal = saldofinal;
            Estado = estado;
            UsuarioAperturaId = usuarioaperturaid;
            UsuarioCierreId = usuariocierreid;
            FechaApertura = fechaapertura;
            FechaCierre = fechacierre;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public float SaldoInicial
        {
            get { return _saldoinicial; }
            set { _saldoinicial = value; }
        }

        public float? SaldoFinal
        {
            get { return _saldofinal; }
            set { _saldofinal = value; }
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

        public int? UsuarioAperturaId
        {
            get { return _usuarioaperturaid; }
            set { _usuarioaperturaid = value; }
        }

        public int? UsuarioCierreId
        {
            get { return _usuariocierreid; }
            set { _usuariocierreid = value; }
        }

        public DateTime FechaApertura
        {
            get { return _fechaapertura; }
            set { _fechaapertura = value; }
        }

        public DateTime? FechaCierre
        {
            get { return _fechacierre; }
            set { _fechacierre = value; }
        }
        #endregion

        #region METODOS
        private void ValidarEstado(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new Exception("El estado no puede ser nulo o vacío");

            if (cadena.Length < MIN_ESTADO || cadena.Length > MAX_ESTADO)
                throw new Exception("El estado debe tener entre 1 y 20 caracteres");

            if (cadena != "Abierta" && cadena != "Cerrada")
                throw new Exception("Estado de caja inválido");
        }
        #endregion
    }
}
