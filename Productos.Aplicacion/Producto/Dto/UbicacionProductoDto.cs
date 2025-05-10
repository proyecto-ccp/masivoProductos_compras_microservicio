

using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Producto.Dto
{
    [ExcludeFromCodeCoverage]
    public class UbicacionProductoDto
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

    [ExcludeFromCodeCoverage]
    public class UbicacionProductoOut : BaseOut
    {
        public UbicacionProductoDto Ubicacion { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaUbicacionProductoOut : BaseOut
    {
        public List<UbicacionProductoDto> Ubicaciones { get; set; }
    }
}
