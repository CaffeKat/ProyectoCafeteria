namespace Cafeteria.DTOs
{
    public class GenerarVentaOutput
    {
        public Guid VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPagado { get; set; }
        public List<DetalleVentaResumen> ProductosVendidos { get; set; } = new();
    }

    public class DetalleVentaResumen
    {
        public string? Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}