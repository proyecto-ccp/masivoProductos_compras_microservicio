
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Entidades
{
    
    public class Producto : EntidadBaseInt
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //proveedor
        public decimal PrecioUnitario { get; set; }
        public Medida Medida { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public Color Color { get; set; }
        public Modelo Modelo { get; set; }
        public Material Material { get; set; }
        public string UrlFoto1 { get; set; }
        public string UrlFoto2 { get; set; }

    }
}
