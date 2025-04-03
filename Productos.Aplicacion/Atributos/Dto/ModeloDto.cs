using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Atributos.Dto
{
    [ExcludeFromCodeCoverage]
    public class ModeloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ModeloOut : BaseOut
    {
        public ModeloDto Modelo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaModelosOut : BaseOut
    {
        public List<ModeloDto> Modelos { get; set; }
    }
}
