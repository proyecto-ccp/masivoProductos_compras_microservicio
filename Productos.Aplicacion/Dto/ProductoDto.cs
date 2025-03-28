namespace Productos.Aplicacion.Dto
{
    public class ProductoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public Guid IdProveedor { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class ProductoOut : BaseOut
    {
        public ProductoDto Producto { get; set; }
    }

    public class ListaProductosOut : BaseOut
    {
        public List<ProductoDto> Productos { get; set; }
    }
}
