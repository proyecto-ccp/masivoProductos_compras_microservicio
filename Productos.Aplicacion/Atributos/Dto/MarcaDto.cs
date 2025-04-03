using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Atributos.Dto
{
    [ExcludeFromCodeCoverage]
    public class MarcaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MarcaOut : BaseOut
    {
        public MarcaDto Marca { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaMarcaOut : BaseOut
    {
        public List<MarcaDto> Marcas { get; set; }
    }
}
