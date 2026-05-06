using Cafeteria.Data;
using Cafeteria.DTOs.Producto;
using Cafeteria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public ProductosController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("ListaDeProductos")]
        public async Task<ActionResult<ICollection<ProductoDTO>>> GetProductos()
        {
            var productos = await _contexto.Productos
                .Include(p => p.Categoria)
                .Select(p => new ProductoDTO
                {
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : "Sin categoría"
                })
                .ToListAsync();

            return Ok(productos);
        }
        [HttpPost("AgregarProducto")]
        public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
        {
            _contexto.Productos.Add(producto);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductos), new { id = producto.Id }, producto);
        }

        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> UpdateProducto(Guid id, [FromBody] Producto producto)
        {
            if (id != producto.Id) return BadRequest();

            var existing = await _contexto.Productos.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Nombre = producto.Nombre;
            existing.Precio = producto.Precio;
            existing.Stock = producto.Stock;    
            existing.CategoriaId = producto.CategoriaId;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}