
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;

namespace Productos.Dominio.Servicios.Atributo
{
    public class ConsultarAtributos(IAtributoRepositorio atributoRepositorio)
    {
        private readonly IAtributoRepositorio _atributoRepositorio = atributoRepositorio;

        public async Task<List<Categoria>> DarCategorias()
        {
            return await _atributoRepositorio.DarCategorias() ?? [];
        }
        public async Task<List<Color>> DarColores()
        {
            return await _atributoRepositorio.DarColores() ?? [];
        }
        public async Task<List<Marca>> DarMarcas()
        {
            return await _atributoRepositorio.DarMarcas() ?? [];
        }
        public async Task<List<Material>> DarMateriales()
        {
            return await _atributoRepositorio.DarMateriales() ?? [];
        }
        public async Task<List<Medida>> DarMedidas()
        {
            return await _atributoRepositorio.DarMedidas() ?? [];
        }
        public async Task<List<Modelo>> DarModelos()
        {
            return await _atributoRepositorio.DarModelos() ?? [];
        }
    }
}
