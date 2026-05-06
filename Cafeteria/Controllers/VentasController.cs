using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Entidades;
using Cafeteria.DTOs;

namespace Cafeteria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly AppDbContext _context;

    public VentasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("GenerarVenta")]
    public async Task<ActionResult<GenerarVentaOutput>> GenerarVenta([FromBody] GenerarVentaInput Output)
    {
        // USAMOS 'Output' (la instancia), NO 'GenerarVentaInput' (la clase)
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Ci == Output.ClienteCi);
        
        if (cliente == null) return NotFound("Cliente no encontrado.");

        var nuevaVenta = new Venta
        {
            Id = Guid.NewGuid(),
            Fecha = DateTime.Now,
            ClienteId = cliente.Id,
            Cliente = cliente,
            Total = 0 
        };

        decimal totalAcumulado = 0;
        var listaDetallesOutput = new List<DetalleVentaResumen>();

        // CAMBIADO: GenerarVentaInput -> Output
        foreach (var item in Output.Detalles)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Nombre == item.NombreProducto);

            if (producto == null)
                return BadRequest($"El producto '{item.NombreProducto}' no existe.");

            if (producto.Stock < item.Cantidad)
                return BadRequest($"Stock insuficiente para {producto.Nombre}. Stock actual: {producto.Stock}");

            var detalleEntidad = new DetalleVenta
            {
                Id = Guid.NewGuid(),
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                VentaId = nuevaVenta.Id,    
                ProductoId = producto.Id,
                Venta = nuevaVenta, 
                Producto = producto 
            };

            producto.Stock -= item.Cantidad;
            totalAcumulado += detalleEntidad.PrecioUnitario * detalleEntidad.Cantidad;

            listaDetallesOutput.Add(new DetalleVentaResumen
            {
                Nombre = producto.Nombre,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio
            });

            _context.DetalleVentas.Add(detalleEntidad);
        }

        nuevaVenta.Total = totalAcumulado;
        _context.Ventas.Add(nuevaVenta);

        await _context.SaveChangesAsync();

        var response = new GenerarVentaOutput
        {
            VentaId = nuevaVenta.Id,
            Fecha = nuevaVenta.Fecha,
            TotalPagado = nuevaVenta.Total,
            ProductosVendidos = listaDetallesOutput
        };

        return Ok(response);
    }
}