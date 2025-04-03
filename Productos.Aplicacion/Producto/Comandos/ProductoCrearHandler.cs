
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Dominio.Servicios.Productos;
using System.Net;

namespace Productos.Aplicacion.Producto.Comandos
{
    public class ProductoCrearHandler : IRequestHandler<ProductoCrear, BaseOut>
    {
        private readonly IMapper _mapper;
        private readonly RegistrarProducto _registrarProducto;

        public ProductoCrearHandler(IMapper mapper, RegistrarProducto registrarProducto)
        {
            _mapper = mapper;
            _registrarProducto = registrarProducto;
        }
        public async Task<BaseOut> Handle(ProductoCrear request, CancellationToken cancellationToken)
        {
            BaseOut output = new();

            try
            {
                var productoNuevo = _mapper.Map<Dominio.Entidades.Producto>(request);
                await _registrarProducto.Crear(productoNuevo);
                output.Resultado = Resultado.Exitoso;
                output.Mensaje = "Producto registrado correctamente";
                output.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = string.Concat("Message: ", ex.Message, ex.InnerException is null ? "" : "-InnerException-"+ex.InnerException.Message);
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }
    }
}
