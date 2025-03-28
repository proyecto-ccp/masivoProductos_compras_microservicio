using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Entidades;

namespace Productos.Dominio.Servicios
{
    public class ObtenerProducto(IProductoRepositorio productoRepositorio)
    {
        private readonly IProductoRepositorio _productoRepositorio = productoRepositorio;

        public async Task<Producto> Ejecutar(Guid id)
        {
            var producto = await _productoRepositorio.ObtenerPorId(id) ?? new Producto();
            return producto;
        }
    }
}
