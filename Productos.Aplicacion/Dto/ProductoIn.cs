namespace Productos.Aplicacion.Dto
{
    public class ProductoIn
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public Guid IdProveedor { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
