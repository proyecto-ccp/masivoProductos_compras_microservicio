
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.Servicios.Productos;
using System.Net;

namespace Productos.Aplicacion.Producto.Consultas
{
    public class ProductoPorIdConsultaHandler : IRequestHandler<ProductoPorIdConsulta, ProductoOut>
    {
        private readonly Consultar _servicio;
        private readonly IMapper _mapper;
        public ProductoPorIdConsultaHandler(Consultar servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }
        public async Task<ProductoOut> Handle(ProductoPorIdConsulta request, CancellationToken cancellationToken)
        {
            ProductoOut output = new ();

            try 
            {
                var producto = await _servicio.EjecutarPorId(request.IdProducto);

                if (producto is null)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontró el producto";
                    output.Status = HttpStatusCode.NoContent;
                }
                else 
                {
                    output.Producto = _mapper.Map<ProductoDto>(producto);
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
