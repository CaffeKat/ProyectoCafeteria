using Cafeteria.Data;
using Cafeteria.DTOs;
using Cafeteria.DTOs.Categoria.ActualizarCategoria;
using Cafeteria.DTOs.Categoria.CrearCategoria;
using Cafeteria.DTOs.Categoria.EliminarCategoria;
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
        [HttpGet("ListaDeCategorias")]
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
        [HttpGet("ObtenerCategoria")]
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
        [HttpPost("AgregarCategoria")]
        public async Task<ActionResult<CrearCategoriaOutput>> CreateCategoria([FromBody] CrearCategoriaInput categoria)
        {
            var entrada = new Categoria
            {
                Id = Guid.NewGuid(),
                Nombre = categoria.Nombre
            };

            _contexto.Categorias.Add(entrada);
             await _contexto.SaveChangesAsync();

            var salida = new CrearCategoriaOutput
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre
            };

            return CreatedAtAction(nameof(CreateCategoria), new { id = salida.Id }, salida);
        }

        // PUT: api/categorias/{id}
        [HttpPut("ActualizarCategoria")]
        public async Task<ActionResult<ActualizarCategoriaOutput>> UpdateCategoria(Guid id, [FromBody] ActualizarCategoriaInput categoria)
        {
            if (id != categoria.Id) return BadRequest("El ID no coincide.");

            var existing = await _contexto.Categorias.FindAsync(id);
            if (existing == null) return NotFound();

            var entrada = new Categoria
            {
                Id = id,
                Nombre = categoria.Nombre
            };

            await _contexto.SaveChangesAsync();

            var salida = new ActualizarCategoriaOutput
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre
            };
            return CreatedAtAction(nameof(UpdateCategoria), new { id = salida.Id }, salida);
        }
    }
}