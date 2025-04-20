
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Repositorios
{
    public interface IParametroRepositorio
    {
        Task<Parametro> DarParametro(string nombre);
    }
}
