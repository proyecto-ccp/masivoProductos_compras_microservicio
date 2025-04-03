
using MediatR;
using Productos.Aplicacion.Atributos.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Productos.Aplicacion.Atributos.Consultas
{
    [ExcludeFromCodeCoverage]
    public record MedidaConsulta() : IRequest<ListaMedidasOut>;

    [ExcludeFromCodeCoverage]
    public record CategoriaConsulta() : IRequest<ListaCategoriaOut>;

    [ExcludeFromCodeCoverage]
    public record MarcaConsulta() : IRequest<ListaMarcaOut>;

    [ExcludeFromCodeCoverage]
    public record ColorConsulta() : IRequest<ListaColorOut>;

    [ExcludeFromCodeCoverage]
    public record ModeloConsulta() : IRequest<ListaModelosOut>;

    [ExcludeFromCodeCoverage]
    public record MaterialConsulta() : IRequest<ListaMaterialOut>;
}
