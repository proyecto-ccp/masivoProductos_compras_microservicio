
using Productos.Dominio.Entidades;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Infraestructura.Adaptadores.RepositorioGenerico;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class ParametroRepositorio : IParametroRepositorio
    {
        private readonly IRepositorioBase<Parametro> _repositorioParametro;

        public ParametroRepositorio(IRepositorioBase<Parametro> repositorioParametro)
        {
            _repositorioParametro = repositorioParametro;
        }
        public async Task<Parametro> DarParametro(string nombre)
        {
            return await _repositorioParametro.BuscarPorLlave(nombre);
        }
    }
}
