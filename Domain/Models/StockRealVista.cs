// Para la vista de verificación
namespace Domain.Models
{
    public class StockRealVista
    {
        public int IngredienteId { get; set; }
        public string Nombre { get; set; }
        public decimal StockEnTabla { get; set; }
        public decimal StockCalculado { get; set; }
        public decimal Diferencia { get; set; }
    }
}