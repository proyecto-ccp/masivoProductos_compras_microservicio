
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Productos.Dominio.ObjetoValor;

namespace Productos.Infraestructura.Adaptadores.Configuraciones
{
    public class UbicacionConfiguracion : IEntityTypeConfiguration<Ubicacion>
    {
        public void Configure(EntityTypeBuilder<Ubicacion> builder)
        {
            builder.HasNoKey();
        }


    }
}
