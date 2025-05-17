

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class UbicacionRespositorio : IUbicacionRespositorio
    {
        private readonly IServiceProvider _serviceProvider;

        public UbicacionRespositorio(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private ProductosDbContext GetContext()
        {
            return _serviceProvider.GetService<ProductosDbContext>();
        }

        public async Task<List<Ubicacion>> ObtenerUbicacionPorIdProducto(int idProducto)
        {
            var ctx = GetContext();
            var ubicaciones = await ctx.Ubicaciones
                .FromSqlRaw("SELECT * FROM fun_productoenbodega({0})", idProducto)
                .ToListAsync();
            await ctx.DisposeAsync();
            return ubicaciones;
        }

    }
}
