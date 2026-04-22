using System;

namespace Cafeteria.Entidades;

public class Categoria
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();

}
