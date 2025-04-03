
using System.Diagnostics.CodeAnalysis;

namespace Productos.Dominio.ObjetoValor
{
    [ExcludeFromCodeCoverage]
    public abstract class AtributoBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
