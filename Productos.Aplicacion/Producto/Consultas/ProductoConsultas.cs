
using MediatR;
using Productos.Aplicacion.Producto.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Producto.Consultas
{
    [ExcludeFromCodeCoverage]
    public record ProductoPorIdConsulta(int IdProducto) : IRequest<ProductoOut>;
    
    [ExcludeFromCodeCoverage]
    public record ProductosConsulta() : IRequest<ListaProductosOut>;

    [ExcludeFromCodeCoverage]
    public record ProductosPorProveedorConsulta(Guid IdProveedor) : IRequest<ListaProductosOut>;
}
