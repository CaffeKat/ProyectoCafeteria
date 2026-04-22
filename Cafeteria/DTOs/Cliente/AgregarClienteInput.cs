namespace Cafeteria.DTOs.Cliente
{
    public class AgregarClienteInput
    {
        public required int Ci { get; set; }
        public string? Extension { get; set; }
        public required string Nombre { get; set; }
    }
}