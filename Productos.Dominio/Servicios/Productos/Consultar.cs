
using Productos.Dominio.Entidades;

namespace Productos.Dominio.Servicios.Productos
{
    public class Consultar (Puertos.Repositorios.IProductoRepositorio productoRepositorio)
    {
        private readonly Puertos.Repositorios.IProductoRepositorio _productoRepositorio = productoRepositorio;

        public async Task<List<Producto>> Ejecutar()
        {
            return await _productoRepositorio.DarListado();
        }

        public async Task<Producto> EjecutarPorId(int id)
        {
            return await _productoRepositorio.ObtenerPorId(id);
        }

        public async Task<List<Producto>> EjecutarPorProveedor(Guid idProveedor)
        {
            return await _productoRepositorio.ObtenerPorProveedor(idProveedor);
        }

        public async Task<Producto> EjecutarPorNombre(string nombre)
        {
            return await _productoRepositorio.ObtenerPorNombre(nombre);
        }
    }
}
