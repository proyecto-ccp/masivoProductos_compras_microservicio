
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.Puertos.Repositorios;
using System.Net;

namespace Productos.Aplicacion.Producto.Consultas
{
    public class ProductosPorProveedorConsultaHandler : IRequestHandler<ProductosPorProveedorConsulta, ListaProductosOut>
    {
        private readonly Consultar _servicio;
        private readonly IMapper _mapper;
        public ProductosPorProveedorConsultaHandler(Consultar servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }
        public async Task<ListaProductosOut> Handle(ProductosPorProveedorConsulta request, CancellationToken cancellationToken)
        {
            ListaProductosOut output = new()
            {
                Productos = []
            };

            try
            {
                var productos = await _servicio.ObtenerPorProveedor(request.IdProveedor) ?? [];

                if (productos.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No hay productos creados para el proveedor";
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
