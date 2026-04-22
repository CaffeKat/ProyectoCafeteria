using System;

namespace Cafeteria.Entidades;

public class Descuentos
{
    public Guid Id {get;set;}
    public required string Nombre {get;set;}
    public decimal Porcentaje {get;set;}
    public ICollection<Venta> Ventas {get;set;} = new List<Venta>(); 
    
}
