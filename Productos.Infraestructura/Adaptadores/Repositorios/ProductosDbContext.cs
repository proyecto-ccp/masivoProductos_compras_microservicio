using Microsoft.EntityFrameworkCore;
using Productos.Dominio.Entidades;
using Productos.Dominio.ObjetoValor;
using Productos.Infraestructura.Adaptadores.Configuraciones;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Infraestructura.Adaptadores.Repositorios
{
    [ExcludeFromCodeCoverage]
    public class ProductosDbContext : DbContext
    {
        public ProductosDbContext(DbContextOptions<ProductosDbContext> options): base(options){ }

        public DbSet<Producto> Productos { get; set;}
        public DbSet<Medida> Medidas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Material> Materiales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguracion());
            modelBuilder.ApplyConfiguration(new ColorConfiguracion());
            modelBuilder.ApplyConfiguration(new MarcaConfiguracion());
            modelBuilder.ApplyConfiguration(new MaterialConfiguracion());
            modelBuilder.ApplyConfiguration(new MedidaConfiguracion());
            modelBuilder.ApplyConfiguration(new ModeloConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());
        }
    }
}
