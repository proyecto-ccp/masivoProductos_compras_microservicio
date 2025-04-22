
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Servicios.Productos;
using Productos.Dominio.Servicios.Stock;
using System.Net;

namespace Productos.Aplicacion.Producto.Comandos
{
    public class ProductoCrearHandler : IRequestHandler<ProductoCrear, BaseOut>
    {
        private readonly IMapper _mapper;
        private readonly RegistrarProducto _registrarProducto;
        private readonly IngresarInventario _ingresarInventario;
        private readonly Consultar _servicio;

        public ProductoCrearHandler(IMapper mapper, RegistrarProducto registrarProducto, IngresarInventario ingresarInventario, Consultar servicio)
        {
            _mapper = mapper;
            _registrarProducto = registrarProducto;
            _ingresarInventario = ingresarInventario;
            _servicio = servicio;
        }
        public async Task<BaseOut> Handle(ProductoCrear request, CancellationToken cancellationToken)
        {
            BaseOut output = new();

            try
            {
                var productoNuevo = _mapper.Map<Dominio.Entidades.Producto>(request);
                
                var productoExiste = await _servicio.EjecutarPorNombre(productoNuevo.Nombre);

                if (productoExiste is null)
                {
                    var productoCreado = await _registrarProducto.Crear(productoNuevo);
                    productoNuevo.Id = productoCreado.Id;
                    output.Mensaje = "Producto registrado correctamente";
                }
                else 
                {
                    productoNuevo.Id = productoExiste.Id;

                }

                IngresarStock ingresarStock = new()
                {
                    IdProducto = productoNuevo.Id,
                    CantidadStock = (int)request.Cantidad,
                };
                
                var procesoInventario = await actualizarInventario(ingresarStock);
                
                output.Resultado = Resultado.Exitoso;
                output.Status = HttpStatusCode.Created;
                output.Mensaje = string.Concat(output.Mensaje, " - ", procesoInventario.Mensaje);

            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = string.Concat("Message: ", ex.Message, ex.InnerException is null ? "" : "-InnerException-"+ex.InnerException.Message);
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }
        private async Task<BaseOut> actualizarInventario(IngresarStock input) 
        {
            BaseOut output = new();

            try
            {
                await _ingresarInventario.Ejecutar(input);
                output.Mensaje = "Inventario actualizado correctamente";
            }
            catch (Exception ex)
            {
                output.Mensaje = string.Concat("Message: ", ex.Message, ex.InnerException is null ? "" : "-InnerException-" + ex.InnerException.Message);
            }

            return output;
        }
    }
}
