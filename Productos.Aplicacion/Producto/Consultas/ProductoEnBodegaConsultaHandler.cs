
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.Servicios.Ubicaciones;
using System.Net;

namespace Productos.Aplicacion.Producto.Consultas
{
    public class ProductoEnBodegaConsultaHandler : IRequestHandler<ProductoEnBodegaConsulta, ListaUbicacionProductoOut>
    {
        private readonly IMapper _mapper;
        private readonly ConsultarUbicacion _servicio;

        public ProductoEnBodegaConsultaHandler(IMapper mapper, ConsultarUbicacion servicio) 
        {
            _mapper = mapper;
            _servicio = servicio;
        }
        public async Task<ListaUbicacionProductoOut> Handle(ProductoEnBodegaConsulta request, CancellationToken cancellationToken)
        {
            ListaUbicacionProductoOut output = new()
            {
                Ubicaciones = []
            };

            try
            {
                var ubicaciones = await _servicio.Ejecutar(request.IdProducto) ?? [];

                if (ubicaciones.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "El producto no se encuentra en las bodegas";
                    output.Status = HttpStatusCode.NotFound;
                }
                else
                {
                    ubicaciones.ForEach(ubicacion => output.Ubicaciones.Add(_mapper.Map<UbicacionProductoDto>(ubicacion)));
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
