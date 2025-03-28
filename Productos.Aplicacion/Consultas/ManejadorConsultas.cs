using AutoMapper;
using Productos.Aplicacion.Dto;
using Productos.Aplicacion.Enum;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios;
using System.Net;

namespace Productos.Aplicacion.Consultas
{
    public class ManejadorConsultas: IConsultasProducto
    {
        private readonly ObtenerProducto _obtenerProducto;
        private readonly ListadoProductos _listadoProductos;
        private readonly IMapper _mapper;

        public ManejadorConsultas(IProductoRepositorio productoRepositorio, IMapper mapper)
        {
            _obtenerProducto = new ObtenerProducto(productoRepositorio);
            _listadoProductos = new ListadoProductos(productoRepositorio);
            _mapper = mapper;
        }

        public async Task<ProductoOut> ObtenerProducto(Guid id)
        {
            ProductoOut productoOut = new();
            try 
            {
                var producto = await _obtenerProducto.Ejecutar(id);

                if(producto.Id == Guid.Empty)
                {
                    productoOut.Resultado = Resultado.SinRegistros;
                    productoOut.Mensaje = "Producto NO encontrado";
                    productoOut.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    productoOut.Producto = _mapper.Map<ProductoDto>(producto);
                    productoOut.Resultado = Resultado.Exitoso;
                    productoOut.Mensaje = "Producto encontrado satisfactoriamente";
                    productoOut.Status = HttpStatusCode.OK;
                }
            }
            catch(Exception ex)
            {
                productoOut.Resultado = Resultado.Error;
                productoOut.Mensaje = ex.Message;
                productoOut.Status = HttpStatusCode.InternalServerError;
            }

            return productoOut;
        }

        public async Task<ListaProductosOut> ObtenerProductos()
        {
            ListaProductosOut output = new()
            {
                Productos = []
            };

            try
            {
                var listadoProductos = await _listadoProductos.Ejecutar();

                if(listadoProductos.Count > 0)
                {
                    listadoProductos.ForEach(producto => output.Productos.Add(_mapper.Map<ProductoDto>(producto)));
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Productos encontrados";
                    output.Status = HttpStatusCode.OK;
                }
                else
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No hay productosd disponibles";
                    output.Status = HttpStatusCode.NoContent;
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
