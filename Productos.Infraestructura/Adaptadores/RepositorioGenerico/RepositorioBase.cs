
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Productos.Dominio.Entidades;
using Productos.Infraestructura.Adaptadores.Repositorios;

namespace Productos.Infraestructura.Adaptadores.RepositorioGenerico
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadBase
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositorioBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private ProductosDbContext GetContext()
        {
            return _serviceProvider.GetService<ProductosDbContext>();
        }

        protected DbSet<T> GetEntitySet()
        {
            return GetContext().Set<T>();
        }
        public async Task<T> BuscarPorLlave(object ValueKey)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.FindAsync(ValueKey);
            await ctx.DisposeAsync();
            return res;
        }

        public async Task<List<T>> DarListado()
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.ToListAsync();
            await ctx.DisposeAsync();
            return res;
        }

        public async Task<T> Guardar(T entity)
        {
            var ctx = GetContext();
            var entitySet = ctx.Set<T>();
            var res = await entitySet.AddAsync(entity);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            return res.Entity;
        }
        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                try
                {
                    var ctx = GetContext();
                    ctx.Dispose();
                }
                catch(Exception ex)
                { }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
