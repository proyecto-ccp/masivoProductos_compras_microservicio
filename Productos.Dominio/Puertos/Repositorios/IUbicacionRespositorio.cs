
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Repositorios
{
    public interface IUbicacionRespositorio
    {
        Task<List<Ubicacion>> ObtenerUbicacionPorIdProducto(int idProducto);
    }
}
