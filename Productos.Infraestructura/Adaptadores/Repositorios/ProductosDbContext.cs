using Microsoft.EntityFrameworkCore;
using Productos.Dominio.Entidades;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    public class ProductosDbContext : DbContext
    {
        public ProductosDbContext(DbContextOptions<ProductosDbContext> options): base(options)
        {

        }

        public DbSet<Producto> Productos { get; set;}
    }
}
