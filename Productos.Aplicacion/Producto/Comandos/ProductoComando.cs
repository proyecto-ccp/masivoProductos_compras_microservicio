
using MediatR;
using Productos.Aplicacion.Comun;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Producto.Comandos
{
    [ExcludeFromCodeCoverage]
    public record ProductoCrear(
        
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        string Nombre,
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        string Descripcion,
        [Required(ErrorMessage = "El campo IdProveedor es obligatorio")]
        Guid? IdProveedor,
        [Required(ErrorMessage = "El campo PrecioUnitario es obligatorio")]
        decimal? PrecioUnitario,
        [Required(ErrorMessage = "El campo IdMedida es obligatorio")]
        int? IdMedida,
        [Required(ErrorMessage = "El campo IdCategoria es obligatorio")]
        int? IdCategoria,
        [Required(ErrorMessage = "El campo IdMarca es obligatorio")]
        int? IdMarca,
        [Required(ErrorMessage = "El campo IdColor es obligatorio")]
        int? IdColor,
        [Required(ErrorMessage = "El campo IdModelo es obligatorio")]
        int? IdModelo,
        [Required(ErrorMessage = "El campo IdMaterial es obligatorio")]
        int? IdMaterial,
        [Required(ErrorMessage = "El campo UrlFoto1 es obligatorio")]
        string UrlFoto1,
        string UrlFoto2,
        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
        int? Cantidad
        ) : IRequest<BaseOut>;
    
}
