using Productos.Aplicacion.Comun;

namespace Productos.Aplicacion.Atributos.Dto
{
    public class ModeloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    public class ModeloOut : BaseOut
    {
        public ModeloDto Modelo { get; set; }
    }

    public class ListaModelosOut : BaseOut
    {
        public List<ModeloDto> Modelos { get; set; }
    }
}
