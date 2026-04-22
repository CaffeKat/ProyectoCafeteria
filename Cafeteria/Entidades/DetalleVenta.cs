using System;

namespace Cafeteria.Entidades;

public class DetalleVenta
{
    public Guid Id { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public Guid VentaId { get; set; }
    public required Venta Venta { get; set; }

    public Guid ProductoId { get; set; }
    public required Producto Producto { get; set; }

}
