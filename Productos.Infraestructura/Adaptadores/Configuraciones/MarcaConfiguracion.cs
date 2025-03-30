
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Productos.Dominio.ObjetoValor;

namespace Productos.Infraestructura.Adaptadores.Configuraciones
{
    public class MarcaConfiguracion : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("tbl_marca");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Codigo)
                .HasColumnName("codigo")
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Codigo)
                .IsUnique();
        }
    }
}
