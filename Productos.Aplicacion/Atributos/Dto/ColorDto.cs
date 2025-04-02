using Productos.Aplicacion.Comun;

namespace Productos.Aplicacion.Atributos.Dto
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    public class ColorOut : BaseOut
    {
        public ColorDto Color { get; set; }
    }

    public class ListaColorOut : BaseOut
    {
        public List<ColorDto> Colores { get; set; }
    }
}
