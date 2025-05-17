
using AutoMapper;
using MediatR;
using Productos.Aplicacion.Comun;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;
using Productos.Dominio.Servicios.Productos;
using Productos.Dominio.Servicios.Stock;
using System.Net;
using System.Text.Json;

namespace Productos.Aplicacion.Producto.Comandos
{
    public class ProductoCrearHandler : IRequestHandler<ProductoCrear, BaseOut>
    {
        private readonly IMapper _mapper;
        private readonly RegistrarProducto _registrarProducto;
        private readonly IngresarInventario _ingresarInventario;
        private readonly Consultar _servicio;
        private readonly IServicioAuditoriaApi _servicioAuditoriaApi;

        public ProductoCrearHandler(IMapper mapper, RegistrarProducto registrarProducto, IngresarInventario ingresarInventario, Consultar servicio, IServicioAuditoriaApi servicioAuditoriaApi)
        {
            _mapper = mapper;
            _registrarProducto = registrarProducto;
            _ingresarInventario = ingresarInventario;
            _servicio = servicio;
            _servicioAuditoriaApi = servicioAuditoriaApi;
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
                    var inputAuditoria = _mapper.Map<Auditoria>(request);
                    inputAuditoria.IdRegistro = productoCreado.Id.ToString();
                    inputAuditoria.Registro = JsonSerializer.Serialize(productoCreado);
                    _ = Task.Run(() => _servicioAuditoriaApi.RegistrarAuditoria(inputAuditoria), cancellationToken);
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
                
                _ = Task.Run(() => ActualizarInventario(ingresarStock, request.Control.Token));
                
                output.Resultado = Resultado.Exitoso;
                output.Status = HttpStatusCode.Created;

            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = string.Concat("Message: ", ex.Message, ex.InnerException is null ? "" : "-InnerException-"+ex.InnerException.Message);
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }
        private async Task<BaseOut> ActualizarInventario(IngresarStock input, string token) 
        {
            BaseOut output = new();

            try
            {
                await _ingresarInventario.Ejecutar(input, token);
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
