using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Atributos.Dto
{
    [ExcludeFromCodeCoverage]
    public class MedidaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MedidaOut : BaseOut
    {
        public MedidaDto Medida { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaMedidasOut : BaseOut
    {
        public List<MedidaDto> Medidas { get; set; }
    }
}
