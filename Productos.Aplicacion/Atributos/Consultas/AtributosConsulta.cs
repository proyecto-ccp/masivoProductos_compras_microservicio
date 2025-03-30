
using MediatR;
using Productos.Aplicacion.Atributos.Dto;

namespace Productos.Aplicacion.Atributos.Consultas
{
    public record MedidaConsulta() : IRequest<ListaMedidasOut>;
    public record CategoriaConsulta() : IRequest<ListaCategoriaOut>;
    public record MarcaConsulta() : IRequest<ListaMarcaOut>;
    public record ColorConsulta() : IRequest<ListaColorOut>;
    public record ModeloConsulta() : IRequest<ListaModelosOut>;
    public record MaterialConsulta() : IRequest<ListaMaterialOut>;
}
