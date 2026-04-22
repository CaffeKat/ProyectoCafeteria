namespace Cafeteria.DTOs
{
    public class GenerarVentaInput
    {
        public required int ClienteCi { get; set; }
        public required List<DetallePedidoInput> Detalles { get; set; }
    }

    public class DetallePedidoInput
    {
        public required string NombreProducto { get; set; }
        public int Cantidad { get; set; }
    }
}