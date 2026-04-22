namespace Cafeteria.DTOs.Cliente
{
    public class ObtenerCliente
    {
        public Guid Id { get; set; }

        public int Ci { get; set; }
        public string? Extension { get; set; }
        public required string Nombre { get; set; }
    }
}