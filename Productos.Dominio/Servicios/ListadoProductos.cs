using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Entidades;

namespace Productos.Dominio.Servicios
{
    public class ListadoProductos(IProductoRepositorio productoRepositorio)
    {
        private readonly IProductoRepositorio _productoRepositorio = productoRepositorio;

        public async Task<List<Producto>> Ejecutar()
        {
            return await _productoRepositorio.DarListado() ?? [];
        }
    }
}
