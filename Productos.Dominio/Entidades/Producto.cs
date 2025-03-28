
using System.ComponentModel.DataAnnotations.Schema;

namespace Productos.Dominio.Entidades
{
    [Table("producto")]
    public class Producto: EntidadBase
    {
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("idproveedor")]
        public Guid IdProveedor { get; set; }

        [Column("preciounitario")]
        public decimal PrecioUnitario { get; set; }
    }
}
