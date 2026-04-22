namespace Cafeteria.DTOs.Cliente
{
    public class AgregarClienteOutput
    {
        public Guid Id { get; set; }
        public required int Ci { get; set; }
        public string? Extension { get; set; }
        public required string Nombre { get; set; }
    }
}