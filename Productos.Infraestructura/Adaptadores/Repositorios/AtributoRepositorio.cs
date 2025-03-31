
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Infraestructura.Adaptadores.RepositorioGenerico;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    public class AtributoRepositorio : IAtributoRepositorio
    {
        private readonly IRepositorioBase<Medida> _medida;
        private readonly IRepositorioBase<Categoria> _categoria;
        private readonly IRepositorioBase<Marca> _marca;
        private readonly IRepositorioBase<Color> _color;
        private readonly IRepositorioBase<Modelo> _modelo;
        private readonly IRepositorioBase<Material> _material;

        public AtributoRepositorio(IRepositorioBase<Medida> medida, IRepositorioBase<Categoria> categoria, IRepositorioBase<Marca> marca, IRepositorioBase<Color> color, IRepositorioBase<Modelo> modelo, IRepositorioBase<Material> material)
        {
            _medida = medida;
            _categoria = categoria;
            _marca = marca;
            _color = color;
            _modelo = modelo;
            _material = material;
        }

        public async Task<Categoria> DarCategoria(int id)
        {
            return await _categoria.BuscarPorLlave(id);
        }

        public async Task<List<Categoria>> DarCategorias()
        {
            return await _categoria.DarListado();
        }

        public async Task<Color> DarColor(int id)
        {
            return await _color.BuscarPorLlave(id);
        }

        public async Task<List<Color>> DarColores()
        {
            return await _color.DarListado();
        }

        public async Task<Marca> DarMarca(int id)
        {
            return await _marca.BuscarPorLlave(id);
        }

        public async Task<List<Marca>> DarMarcas()
        {
            return await _marca.DarListado();
        }

        public async Task<Material> DarMaterial(int id)
        {
            return await _material.BuscarPorLlave(id);
        }

        public async Task<List<Material>> DarMateriales()
        {
            return await _material.DarListado();
        }

        public async Task<Medida> DarMedida(int id)
        {
            return await _medida.BuscarPorLlave(id);
        }

        public async Task<List<Medida>> DarMedidas()
        {
            return await _medida.DarListado();
        }

        public async Task<Modelo> DarModelo(int id)
        {
            return await _modelo.BuscarPorLlave(id);
        }

        public async Task<List<Modelo>> DarModelos()
        {
            return await _modelo.DarListado();
        }
    }
}
