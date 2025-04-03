
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Repositorios
{
    public interface IAtributoRepositorio
    {
        Task<List<Categoria>> DarCategorias();
        Task<Categoria> DarCategoria(int id);
        Task<List<Color>> DarColores();
        Task<Color> DarColor(int id);
        Task<List<Marca>> DarMarcas();
        Task<Marca> DarMarca(int id);
        Task<List<Material>> DarMateriales();
        Task<Material> DarMaterial(int id);
        Task<List<Medida>> DarMedidas();
        Task<Medida> DarMedida(int id);
        Task<List<Modelo>> DarModelos();
        Task<Modelo> DarModelo(int id);

    }
}
