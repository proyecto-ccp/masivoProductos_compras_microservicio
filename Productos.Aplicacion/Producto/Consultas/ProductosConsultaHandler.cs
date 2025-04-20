
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.Puertos.Repositorios;
using System.Net;

namespace Productos.Aplicacion.Producto.Consultas
{
    public class ProductosConsultaHandler : IRequestHandler<ProductosConsulta, ListaProductosOut>
    {
        private readonly Consultar _servicio;
        private readonly IMapper _mapper;
        public ProductosConsultaHandler(Consultar servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }
        public async Task<ListaProductosOut> Handle(ProductosConsulta request, CancellationToken cancellationToken)
        {
            ListaProductosOut output = new()
            {
                Productos = []
            };

            try 
            {
                var productos = await _servicio.DarListado() ?? [];

                if (productos.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No hay productos creados";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    productos.ForEach(producto => output.Productos.Add(_mapper.Map<ProductoDto>(producto)));
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Consulta exitosa";
                    output.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = ex.Message;
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;

        }
    }
}
