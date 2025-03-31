

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Productos.Dominio.Entidades;

namespace Productos.Infraestructura.Adaptadores.Configuraciones
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("tbl_productos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Descripcion)
                .HasColumnName("descripcion")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.PrecioUnitario)
                .HasColumnName("precioUnitario")
                .IsRequired(); 

            builder.Property(x => x.UrlFoto1)
                .HasColumnName("urlFoto1")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.UrlFoto2)
                .HasColumnName("urlFoto2")
                .HasMaxLength(255);

            builder.HasIndex(x => x.Nombre)
                .IsUnique();

            builder.HasOne(x => x.Proveedor)
                .WithMany()
                .HasForeignKey("idProveedor")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Medida)
                .WithMany()
                .HasForeignKey("idMedida")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey("idCategoria")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Marca)
                .WithMany()
                .HasForeignKey("idMarca")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Color)
                .WithMany()
                .HasForeignKey("idColor")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Modelo)
                .WithMany()
                .HasForeignKey("idModelo")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Material)
                .WithMany()
                .HasForeignKey("idMaterial")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
