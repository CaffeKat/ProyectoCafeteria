using System;

namespace Cafeteria.Entidades;

public class Venta
{
    public Guid Id { get; set; }
    public required DateTime Fecha { get; set; }
    public required decimal Total { get; set; }

    public Guid ClienteId { get; set; }
    public Guid? Descuentoid {get;set;}
    public Descuentos? Descuento {get;set;}
    public required Cliente Cliente { get; set; }
}
