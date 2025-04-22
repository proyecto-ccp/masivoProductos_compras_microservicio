
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
    public class ProductosConsultaHandlerTest
    {
        private readonly Consultar _servicio;
        private readonly IMapper _mapper;
        private readonly Mock<IProductoRepositorio> _mockProductoRepositorio;

        public ProductosConsultaHandlerTest() 
        {
            _mockProductoRepositorio = new Mock<IProductoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProductoMapeador()));
            _mapper = config.CreateMapper();
            _servicio = new Consultar(_mockProductoRepositorio.Object);
        }

        [Theory]
        [InlineData(Resultado.Exitoso, "Consulta exitosa", HttpStatusCode.OK)]
        [InlineData(Resultado.SinRegistros, "No hay productos creados", HttpStatusCode.NoContent)]
        [InlineData(Resultado.Error, "Error", HttpStatusCode.InternalServerError)]
        public async Task Handle_ValidaRespuestas(Resultado res, string msj, HttpStatusCode status)
        {
            List<Producto> productos =
                [
                    new Producto
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
                    },
                    new Producto
                    {
                        Id = 2,
                        Nombre = "Producto de prueba 2",
                        Descripcion = "Descripcion de prueba 2",
                        PrecioUnitario = 200.00m,
                        IdProveedor = Guid.NewGuid(),
                        IdCategoria = 1,
                        IdMarca = 1,
                        IdMedida = 1,
                        UrlFoto1 = "http://example.com/foto2.jpg",
                        UrlFoto2 = "http://example.com/foto3.jpg"
                    }
                ];

            if (res == Resultado.Exitoso)
            {
                _mockProductoRepositorio.Setup(m => m.DarListado()).ReturnsAsync(productos);
            }
            else if (res == Resultado.Error)
            {
                _mockProductoRepositorio.Setup(m => m.DarListado()).ThrowsAsync(new Exception(msj));
            }


            var objPrueba = new ProductosConsultaHandler(_servicio, _mapper);

            var request = new ProductosConsulta();
            var result = await objPrueba.Handle(request, CancellationToken.None);

            Assert.Equal(res, result.Resultado);
            Assert.Contains(msj, result.Mensaje);
            Assert.Equal(status, result.Status);

            if (res == Resultado.Exitoso)
            {
                Assert.NotNull(result.Productos);
                Assert.NotEmpty(result.Productos);
                Assert.Equal(2, result.Productos.Count);
            }
            else if (res == Resultado.SinRegistros)
            {
                Assert.Empty(result.Productos);
            }

        }

    }
}
