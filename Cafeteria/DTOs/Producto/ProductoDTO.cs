namespace Cafeteria.DTOs.Producto
{
    public class ProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty; // En lugar del ID, mandamos el nombre
    }
}