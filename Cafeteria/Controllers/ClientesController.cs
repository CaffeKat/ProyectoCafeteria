using Cafeteria.Data;
using Cafeteria.DTOs.Cliente;
using Cafeteria.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public ClientesController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ObtenerCliente>>> GetClientes()
        {
            var clientes = await _contexto.Clientes
            .Select(c => new ObtenerCliente
            {
                Id = c.Id,
                Ci = c.Ci,
                Extension = c.Extension,
                Nombre = c.Nombre
            })
            .ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _contexto.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<AgregarClienteInput>> CreateCliente([FromBody] AgregarClienteOutput cliente)
        {
            var entrada = new Cliente
            {
                Ci = cliente.Ci,
                Extension = cliente.Extension,
                Nombre = cliente.Nombre
            };

            entrada.Id = Guid.NewGuid();
            entrada.FechaCreacion = DateTime.Now;
            entrada.FechaUltimaModificacion = DateTime.Now;
            entrada.EsClientePorDefecto = false;

             _contexto.Clientes.Add(entrada);
            await _contexto.SaveChangesAsync();

            var salida = new AgregarClienteOutput
            {
                Id = entrada.Id,
                Nombre = entrada.Nombre,
                Ci = entrada.Ci,
                Extension = entrada.Extension
            };

            return CreatedAtAction(nameof(GetCliente), new { id = salida.Id }, salida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(Guid id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();

            var existing = await _contexto.Clientes.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Nombre = cliente.Nombre;
            existing.Ci = cliente.Ci;
            existing.Extension = cliente.Extension;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            var cliente = await _contexto.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}