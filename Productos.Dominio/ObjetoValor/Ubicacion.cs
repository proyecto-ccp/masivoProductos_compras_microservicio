
using System.Diagnostics.CodeAnalysis;

namespace Productos.Dominio.ObjetoValor
{
    [ExcludeFromCodeCoverage]
    public class Ubicacion
    {
        public Guid Idciudad { get; set; }
        public string Ciudad { get; set; }
        public int Idproducto { get; set; }
        public string Producto { get; set; }
        public int IdBodega { get; set; }
        public string Bodega { get; set; }
        public int Pasillo { get; set; }
        public int Estante { get; set; }
        public int Nivel { get; set; }
    }
}
