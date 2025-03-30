using Productos.Aplicacion.Dto;

namespace Productos.Aplicacion.Atributos.Dto
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    public class CategoriaOut : BaseOut
    {
        public CategoriaDto Categoria { get; set; }
    }

    public class ListaCategoriaOut : BaseOut
    {
        public List<CategoriaDto> Categorias { get; set; }
    }
}
