namespace Cafeteria.DTOs.Categoria.ActualizarCategoria
{
    public class ActualizarCategoriaInput
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
    }
}