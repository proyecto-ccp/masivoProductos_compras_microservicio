
using System.Diagnostics.CodeAnalysis;

namespace Productos.Dominio.ObjetoValor
{
    [ExcludeFromCodeCoverage]
    public class IngresarStock
    {
        public int IdProducto { get; set; }
        public int CantidadStock { get; set; }
    }
}
