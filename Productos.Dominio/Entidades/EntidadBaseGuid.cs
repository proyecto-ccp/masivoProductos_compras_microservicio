

using System.Diagnostics.CodeAnalysis;

namespace Productos.Dominio.Entidades
{
    [ExcludeFromCodeCoverage]
    public abstract class EntidadBaseGuid
    {
        public Guid Id { get; set; }

    }
}
