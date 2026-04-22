using System;

namespace Cafeteria.Entidades;

public class Producto
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public Guid CategoriaId { get; set; }
    public required Categoria Categoria { get; set; }

    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

}
