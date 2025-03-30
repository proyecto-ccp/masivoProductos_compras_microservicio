

namespace Productos.Infraestructura.Adaptadores.RepositorioGenerico
{
    public interface IRepositorioBase<T> : IDisposable where T : class
    {
        Task<T> Guardar(T entity);
        Task<T> BuscarPorLlave(object ValueKey);
        Task<List<T>> DarListado();
    }
}
