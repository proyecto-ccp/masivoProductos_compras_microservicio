using Productos.Aplicacion.Comun;

namespace Productos.Aplicacion.Atributos.Dto
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
    public class MaterialOut : BaseOut
    {
        public MaterialDto Material { get; set; }
    }

    public class ListaMaterialOut : BaseOut
    {
        public List<MaterialDto> Materiales { get; set; }
    }
}
