using Productos.Dominio.Entidades;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Infraestructura.Adaptadores.RepositorioGenerico;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
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

        public async Task<Producto> Guardar(Producto producto)
        {
            return await _repositorioProducto.Guardar(producto);
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _repositorioProducto.BuscarPorLlave(id);
        }

        public async Task<Producto> ObtenerPorNombre(string nombre)
        {
            return await _repositorioProducto.BuscarPorCampos(x => x.Nombre == nombre);
        }

        public async Task<List<Producto>> ObtenerPorProveedor(Guid idProveedor)
        {
            return await _repositorioProducto.DarListadoPorCampos(x => x.IdProveedor == idProveedor);
        }

    }
}
