using Productos.Dominio.Entidades;

namespace Productos.Dominio.Puertos.Repositorios
{
    public interface IProductoRepositorio
    {
        Task<Producto> Guardar(Producto producto);
        Task<Producto> ObtenerPorId(int id);
        Task<List<Producto>> DarListado();
        Task<List<Producto>> ObtenerPorProveedor(Guid idProveedor);
        Task<Producto> ObtenerPorNombre(string nombre);

    }
}
