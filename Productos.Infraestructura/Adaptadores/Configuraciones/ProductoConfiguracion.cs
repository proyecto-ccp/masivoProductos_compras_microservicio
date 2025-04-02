

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
                .HasColumnName("preciounitario")
                .IsRequired(); 

            builder.Property(x => x.UrlFoto1)
                .HasColumnName("urlfoto1")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.UrlFoto2)
                .HasColumnName("urlfoto2")
                .HasMaxLength(255);

            builder.HasIndex(x => x.Nombre)
                .IsUnique();

            builder.Property(x => x.IdProveedor)
                .HasColumnName("idproveedor")
                .IsRequired();

            builder.Property(x => x.IdCategoria)
                .HasColumnName("idcategoria")
                .IsRequired();

            builder.Property(x => x.IdMedida)
                .HasColumnName("idmedida")
                .IsRequired();

            builder.Property(x => x.IdMarca)
                .HasColumnName("idmarca")
                .IsRequired();

            builder.Property(x => x.IdColor)
                .HasColumnName("idcolor")
                .IsRequired();

            builder.Property(x => x.IdModelo)
                .HasColumnName("idmodelo")
                .IsRequired();

            builder.Property(x => x.IdMaterial)
                .HasColumnName("idmaterial")
                .IsRequired();

        }
    }
}
