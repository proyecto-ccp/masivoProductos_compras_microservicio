using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;


namespace Productos.Aplicacion.Producto.Dto
{
    [ExcludeFromCodeCoverage]
    public class ProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid IdProveedor { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdMedida { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdColor { get; set; }
        public int IdModelo { get; set; }
        public int IdMaterial { get; set; }
        public string UrlFoto1 { get; set; }
        public string UrlFoto2 { get; set; }

    }

    [ExcludeFromCodeCoverage]
    public class ProductoOut : BaseOut
    {
        public ProductoDto Producto { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaProductosOut : BaseOut
    {
        public List<ProductoDto> Productos { get; set; }
    }
}
