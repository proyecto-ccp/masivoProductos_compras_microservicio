using Productos.Aplicacion.Comun;

namespace Productos.Aplicacion.Atributos.Dto
{
    public class MarcaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
    public class MarcaOut : BaseOut
    {
        public MarcaDto Marca { get; set; }
    }

    public class ListaMarcaOut : BaseOut
    {
        public List<MarcaDto> Marcas { get; set; }
    }
}
