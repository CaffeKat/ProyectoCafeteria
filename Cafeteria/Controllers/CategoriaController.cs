using Cafeteria.Data;
using Cafeteria.DTOs;
using Cafeteria.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public CategoriasController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObtenerCategoriaDTO>>> GetCategorias()
        {
            var categorias = await _contexto.Categorias
                .Select(c => new ObtenerCategoriaDTO 
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // GET: api/categorias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ObtenerCategoriaDTO>> GetCategoria(Guid id) // Cambiado a CategoriaDTO
        {
            var categoria = await _contexto.Categorias
                .Where(c => c.Id == id) // Filtramos primero
                .Select(c => new ObtenerCategoriaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .FirstOrDefaultAsync(); // Usamos FirstOrDefaultAsync en lugar de FindAsync

            if (categoria == null) 
            {
                return NotFound();
            }
            
            return Ok(categoria);
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria categoria)
        {
            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        // PUT: api/categorias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(Guid id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id) return BadRequest("El ID no coincide.");

            var existing = await _contexto.Categorias.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Nombre = categoria.Nombre;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/categorias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}