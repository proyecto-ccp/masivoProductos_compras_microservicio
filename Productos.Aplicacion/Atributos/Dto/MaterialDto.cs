using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Atributos.Dto
{
    [ExcludeFromCodeCoverage]
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MaterialOut : BaseOut
    {
        public MaterialDto Material { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaMaterialOut : BaseOut
    {
        public List<MaterialDto> Materiales { get; set; }
    }
}
