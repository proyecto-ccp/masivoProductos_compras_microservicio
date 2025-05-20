
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Comandos;
using Productos.Aplicacion.Producto.Consultas;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.Servicios.Ubicaciones;
using ServicioProducto.Api.Controllers;
using System.Net;

namespace Productos.Tests.Controllers
{
    public class ProductosControllerTest
    {
        private readonly Mock<IMediator> mockMediator;

        public ProductosControllerTest()
        {
            mockMediator = new Mock<IMediator>();
        }

        [Theory]
        [InlineData(Resultado.Exitoso, HttpStatusCode.Created)]
        [InlineData(Resultado.Error, HttpStatusCode.InternalServerError)]
        public async Task Crear_respuestas(Resultado enumRes, HttpStatusCode status)
        {
            var output = new BaseOut
            {
                Resultado = enumRes,
                Status = status
            };

            mockMediator.Setup(m => m.Send(It.IsAny<ProductoCrear>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var objPrueba = new ProductosController(mockMediator.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Authorization = "Bearer pruebas-token-123";
            httpContext.Items["UserId"] = Guid.NewGuid().ToString();
            objPrueba.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var baseIn = new BaseIn
            {
                Token = "tokenPruebasUnitarias",
                IdUsuario = Guid.NewGuid().ToString(),
            };
            var request = new ProductoCrear
            (
                "Pruebas",
                "Descripcion de prueba",
                Guid.NewGuid(),
                100.00m,
                1,
                1,
                1,
                1,
                1,
                1,
                "http://example.com/foto1.jpg",
                "http://example.com/foto2.jpg",
                100,
                baseIn
            );

            var resultado = await objPrueba.Crear(request);

            Assert.NotNull(resultado);

            if (enumRes == Resultado.Exitoso)
            {
                var verResultado = Assert.IsType<CreatedResult>(resultado);
                var res = verResultado.Value as BaseOut;
                Assert.IsType<BaseOut>(res);
                Assert.Equal(201, verResultado.StatusCode);
            }
            else
            {
                var verResultado = Assert.IsType<ObjectResult>(resultado);
                var res = verResultado.Value as ProblemDetails;
                Assert.IsType<ProblemDetails>(res);
                Assert.Equal(500, verResultado.StatusCode);
            }
        }

        [Theory]
        [InlineData(Resultado.Exitoso, HttpStatusCode.OK)]
        [InlineData(Resultado.Error, HttpStatusCode.InternalServerError)]
        [InlineData(Resultado.SinRegistros, HttpStatusCode.NoContent)]
        public async Task Consultar_respuestas(Resultado enumRes, HttpStatusCode status)
        {
            var output = new ListaProductosOut
            {
                Resultado = enumRes,
                Status = status
            };

            mockMediator.Setup(m => m.Send(It.IsAny<ProductosConsulta>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var objPrueba = new ProductosController(mockMediator.Object);

            var request = new ProductosConsulta();

            var resultado = await objPrueba.Consultar();

            Assert.NotNull(resultado);

            if (enumRes == Resultado.Exitoso)
            {
                var verResultado = Assert.IsType<OkObjectResult>(resultado);
                var res = verResultado.Value as ListaProductosOut;
                Assert.IsType<ListaProductosOut>(res);
                Assert.Equal(200, verResultado.StatusCode);
            }
            else if (enumRes == Resultado.Error)
            {
                var verResultado = Assert.IsType<ObjectResult>(resultado);
                var res = verResultado.Value as ProblemDetails;
                Assert.IsType<ProblemDetails>(res);
                Assert.Equal(500, verResultado.StatusCode);
            }
            else 
            {
                var verResultado = Assert.IsType<NoContentResult>(resultado);
                Assert.Equal(204, verResultado.StatusCode);

            }
        }

        [Theory]
        [InlineData(Resultado.Exitoso, HttpStatusCode.OK)]
        [InlineData(Resultado.Error, HttpStatusCode.InternalServerError)]
        [InlineData(Resultado.SinRegistros, HttpStatusCode.NoContent)]
        public async Task ConsultarPorProveedor_respuestas(Resultado enumRes, HttpStatusCode status)
        {
            var output = new ListaProductosOut
            {
                Resultado = enumRes,
                Status = status
            };

            mockMediator.Setup(m => m.Send(It.IsAny<ProductosPorProveedorConsulta>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var objPrueba = new ProductosController(mockMediator.Object);

            var request = new ProductosPorProveedorConsulta(Guid.NewGuid());

            var resultado = await objPrueba.ConsultarPorProveedor(request);

            Assert.NotNull(resultado);

            if (enumRes == Resultado.Exitoso)
            {
                var verResultado = Assert.IsType<OkObjectResult>(resultado);
                var res = verResultado.Value as ListaProductosOut;
                Assert.IsType<ListaProductosOut>(res);
                Assert.Equal(200, verResultado.StatusCode);
            }
            else if (enumRes == Resultado.Error)
            {
                var verResultado = Assert.IsType<ObjectResult>(resultado);
                var res = verResultado.Value as ProblemDetails;
                Assert.IsType<ProblemDetails>(res);
                Assert.Equal(500, verResultado.StatusCode);
            }
            else
            {
                var verResultado = Assert.IsType<NoContentResult>(resultado);
                Assert.Equal(204, verResultado.StatusCode);

            }
        }

        [Theory]
        [InlineData(Resultado.Exitoso, HttpStatusCode.OK)]
        [InlineData(Resultado.Error, HttpStatusCode.InternalServerError)]
        [InlineData(Resultado.SinRegistros, HttpStatusCode.NoContent)]
        public async Task ConsultarPorId_respuestas(Resultado enumRes, HttpStatusCode status)
        {
            var output = new ProductoOut
            {
                Resultado = enumRes,
                Status = status
            };

            mockMediator.Setup(m => m.Send(It.IsAny<ProductoPorIdConsulta>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var objPrueba = new ProductosController(mockMediator.Object);

            var request = new ProductoPorIdConsulta(111);

            var resultado = await objPrueba.ConsultarPorId(request);

            Assert.NotNull(resultado);

            if (enumRes == Resultado.Exitoso)
            {
                var verResultado = Assert.IsType<OkObjectResult>(resultado);
                var res = verResultado.Value as ProductoOut;
                Assert.IsType<ProductoOut>(res);
                Assert.Equal(200, verResultado.StatusCode);
            }
            else if (enumRes == Resultado.Error)
            {
                var verResultado = Assert.IsType<ObjectResult>(resultado);
                var res = verResultado.Value as ProblemDetails;
                Assert.IsType<ProblemDetails>(res);
                Assert.Equal(500, verResultado.StatusCode);
            }
            else
            {
                var verResultado = Assert.IsType<NoContentResult>(resultado);
                Assert.Equal(204, verResultado.StatusCode);

            }
        }

        [Theory]
        [InlineData(Resultado.Exitoso, HttpStatusCode.OK)]
        [InlineData(Resultado.Error, HttpStatusCode.InternalServerError)]
        [InlineData(Resultado.SinRegistros, HttpStatusCode.NotFound)]
        public async Task ConsultarUbicacion_respuestas(Resultado enumRes, HttpStatusCode status)
        {
            var output = new ListaUbicacionProductoOut
            {
                Resultado = enumRes,
                Status = status
            };

            mockMediator.Setup(m => m.Send(It.IsAny<ProductoEnBodegaConsulta>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var objPrueba = new ProductosController(mockMediator.Object);

            var request = new ProductoEnBodegaConsulta(11);

            var resultado = await objPrueba.ConsultarUbicacion(11);

            Assert.NotNull(resultado);

            if (enumRes == Resultado.Exitoso)
            {
                var verResultado = Assert.IsType<OkObjectResult>(resultado);
                var res = verResultado.Value as ListaUbicacionProductoOut;
                Assert.IsType<ListaUbicacionProductoOut>(res);
                Assert.Equal(200, verResultado.StatusCode);
            }
            else if (enumRes == Resultado.Error)
            {
                var verResultado = Assert.IsType<ObjectResult>(resultado);
                var res = verResultado.Value as ProblemDetails;
                Assert.IsType<ProblemDetails>(res);
                Assert.Equal(500, verResultado.StatusCode);
            }
            else
            {
                var verResultado = Assert.IsType<NotFoundObjectResult>(resultado);
                Assert.Equal(404, verResultado.StatusCode);

            }
        }

    }
}
