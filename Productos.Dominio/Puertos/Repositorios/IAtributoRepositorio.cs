
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Repositorios
{
    public interface IAtributoRepositorio
    {
        Task<List<Categoria>> DarCategorias();
        Task<List<Color>> DarColores();
        Task<List<Marca>> DarMarcas();
        Task<List<Material>> DarMateriales();
        Task<List<Medida>> DarMedidas();
        Task<List<Modelo>> DarModelos();

    }
}
