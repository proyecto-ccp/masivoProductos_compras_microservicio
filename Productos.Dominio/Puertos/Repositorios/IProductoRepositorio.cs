using Productos.Dominio.Entidades;

namespace Productos.Dominio.Puertos.Repositorios
{
    public interface IProductoRepositorio
    {
        Task Guardar(Producto producto);
        Task<Producto> ObtenerPorId(Guid id);
        Task<List<Producto>> DarListado();

    }
}
