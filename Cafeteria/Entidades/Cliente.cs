using System;

namespace Cafeteria.Entidades;

public class Cliente
{

public Guid Id { get; set; }
    public required int Ci { get; set; }
    public string? Extension { get; set; }
    public required string Nombre { get; set; }
    public bool? EsClientePorDefecto { get; set; }
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    public DateTime FechaCreacion { get; internal set; }
    public DateTime FechaUltimaModificacion { get; internal set; }
    
}
