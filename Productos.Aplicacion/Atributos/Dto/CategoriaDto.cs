using Productos.Aplicacion.Comun;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Atributos.Dto
{
    [ExcludeFromCodeCoverage]
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class CategoriaOut : BaseOut
    {
        public CategoriaDto Categoria { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ListaCategoriaOut : BaseOut
    {
        public List<CategoriaDto> Categorias { get; set; }
    }
}
