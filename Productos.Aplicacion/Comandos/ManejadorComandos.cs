using AutoMapper;
using Productos.Aplicacion.Dto;
using Productos.Aplicacion.Enum;
using Productos.Dominio.Entidades;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios;
using System.Net;

namespace Productos.Aplicacion.Comandos
{
    public class ManejadorComandos : IComandosProducto
    {
        private readonly CrearProducto _crearProducto;
        private readonly IMapper _mapper;

        public ManejadorComandos(IProductoRepositorio productoRepositorio, IMapper mapper)
        {
            _crearProducto = new CrearProducto(productoRepositorio);
            _mapper = mapper;
        }
        public async Task<BaseOut> CrearProducto(ProductoIn producto)
        {
            BaseOut baseOut = new();

            try 
            {
                var productoDominio = _mapper.Map<Producto>(producto);
                await _crearProducto.Ejecutar(productoDominio);
                baseOut.Resultado = Resultado.Exitoso;
                baseOut.Mensaje = "Producto creado exitosamente";
                baseOut.Status = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                baseOut.Resultado = Resultado.Error;
                baseOut.Mensaje = ex.Message;
                baseOut.Status = HttpStatusCode.InternalServerError;
            }

            return baseOut;
        }
    }
}
