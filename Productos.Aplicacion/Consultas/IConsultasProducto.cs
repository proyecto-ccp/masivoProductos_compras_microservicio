
using Productos.Aplicacion.Dto;

namespace Productos.Aplicacion.Consultas
{
    public interface IConsultasProducto
    {
        public Task<ProductoOut> ObtenerProducto(Guid id);
        public Task<ListaProductosOut> ObtenerProductos();
    }
}
