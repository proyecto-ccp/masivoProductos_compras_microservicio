
using AutoMapper;
using Moq;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Consultas;
using Productos.Aplicacion.Producto.Mapeadores;
using Productos.Dominio.Entidades;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios.Productos;
using System.Net;

namespace Productos.Tests.Aplicacion.Consultas
{
    public class ProductoPorIdConsultaHandlerTest
    {
        private readonly Consultar _servicio;
        private readonly IMapper _mapper;
        private readonly Mock<IProductoRepositorio> _mockProductoRepositorio;

        public ProductoPorIdConsultaHandlerTest() 
        {
            _mockProductoRepositorio = new Mock<IProductoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProductoMapeador()));
            _mapper = config.CreateMapper();
            _servicio = new Consultar(_mockProductoRepositorio.Object);
        }

        [Theory]
        [InlineData(Resultado.Exitoso, "Consulta exitosa", HttpStatusCode.OK)]
        [InlineData(Resultado.SinRegistros, "No se encontró el producto", HttpStatusCode.NoContent)]
        [InlineData(Resultado.Error, "Error", HttpStatusCode.InternalServerError)]
        public async Task Handle_ValidaRespuestas(Resultado res, string msj, HttpStatusCode status) 
        {
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Producto de prueba",
                Descripcion = "Descripcion de prueba",
                PrecioUnitario = 100.00m,
                IdProveedor = Guid.NewGuid(),
                IdCategoria = 1,
                IdMarca = 1,
                IdMedida = 1,
                UrlFoto1 = "http://example.com/foto1.jpg",
                UrlFoto2 = "http://example.com/foto2.jpg"
            };

            if (res == Resultado.Exitoso)
            {
                _mockProductoRepositorio.Setup(m => m.ObtenerPorId(It.IsAny<int>())).ReturnsAsync(producto);
            }
            else if (res == Resultado.Error)
            {
                _mockProductoRepositorio.Setup(m => m.ObtenerPorId(It.IsAny<int>())).ThrowsAsync(new Exception(msj));
            }
            

            var objPrueba = new ProductoPorIdConsultaHandler(_servicio, _mapper);

            var request = new ProductoPorIdConsulta(1);
            var result = await objPrueba.Handle(request, CancellationToken.None);

            Assert.Equal(res, result.Resultado);
            Assert.Contains(msj, result.Mensaje);
            Assert.Equal(status, result.Status);

            if (res == Resultado.Exitoso)
            {
                Assert.NotNull(result.Producto);
                Assert.Equal(producto.Id, result.Producto.Id);
            }
            else if (res == Resultado.SinRegistros) 
            {
                Assert.Null(result.Producto);
            }

        }
    }
}
