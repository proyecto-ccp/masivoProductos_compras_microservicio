using Productos.Dominio.Entidades;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Infraestructura.Adaptadores.RepositorioGenerico;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly IRepositorioBase<Producto> _repositorioProducto;

        public ProductoRepositorio(IRepositorioBase<Producto> repositorioProducto)
        {
            _repositorioProducto = repositorioProducto;
        }
        public async Task<List<Producto>> DarListado()
        {
            return await _repositorioProducto.DarListado();
        }

        public async Task Guardar(Producto producto)
        {
            await _repositorioProducto.Guardar(producto);
        }

        public async Task<Producto> ObtenerPorId(Guid id)
        {
            return await _repositorioProducto.BuscarPorLlave(id);
        }
    }
}
