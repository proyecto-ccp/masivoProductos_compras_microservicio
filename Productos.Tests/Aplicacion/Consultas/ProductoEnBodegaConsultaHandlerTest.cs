
using AutoMapper;
using Moq;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Consultas;
using Productos.Aplicacion.Producto.Mapeadores;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios.Ubicaciones;
using System.Net;

namespace Productos.Tests.Aplicacion.Consultas
{
    public class ProductoEnBodegaConsultaHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ConsultarUbicacion _servicio;
        private readonly Mock<IUbicacionRespositorio> _mockUbicacionRepositorio;

        public ProductoEnBodegaConsultaHandlerTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new UbicacionMapeador()));
            _mapper = config.CreateMapper();
            _mockUbicacionRepositorio = new Mock<IUbicacionRespositorio>();
            _servicio = new ConsultarUbicacion(_mockUbicacionRepositorio.Object);
        }

        [Theory]
        [InlineData(Resultado.Exitoso, "Consulta exitosa", HttpStatusCode.OK)]
        [InlineData(Resultado.SinRegistros, "El producto no se encuentra en las bodegas", HttpStatusCode.NotFound)]
        [InlineData(Resultado.Error, "Error", HttpStatusCode.InternalServerError)]
        public async Task Handle_ValidaRespuestas(Resultado res, string msj, HttpStatusCode status)
        {
            List<Ubicacion> output = 
                [
                    new ()
                    { Idciudad = Guid.NewGuid(), Ciudad = "Ciudad 1", Idproducto = 1, Producto = "Producto 1", IdBodega = 1, Bodega = "Bodega 1", Pasillo = 1, Estante = 1, Nivel = 1 },
                    new ()
                    { Idciudad = Guid.NewGuid(), Ciudad = "Ciudad 2", Idproducto = 1, Producto = "Producto 2", IdBodega = 2, Bodega = "Bodega 2", Pasillo = 2, Estante = 2, Nivel = 2 },
                ];

            if (res == Resultado.Exitoso)
            {
                _mockUbicacionRepositorio.Setup(m => m.ObtenerUbicacionPorIdProducto(It.IsAny<int>())).ReturnsAsync(output);
            }
            else if (res == Resultado.Error)
            {
                _mockUbicacionRepositorio.Setup(m => m.ObtenerUbicacionPorIdProducto(It.IsAny<int>())).ThrowsAsync(new Exception(msj));
            }

            var objPrueba = new ProductoEnBodegaConsultaHandler(_mapper, _servicio);
            var request = new ProductoEnBodegaConsulta(1);

            var result = await objPrueba.Handle(request, CancellationToken.None);

            Assert.Equal(res, result.Resultado);
            Assert.Contains(msj, result.Mensaje);
            Assert.Equal(status, result.Status);

            if (res == Resultado.Exitoso)
            {
                Assert.NotNull(result.Ubicaciones);
                Assert.NotEmpty(result.Ubicaciones);
                Assert.Equal(2, result.Ubicaciones.Count);
            }
            else if (res == Resultado.SinRegistros)
            {
                Assert.Empty(result.Ubicaciones);
            }
        }

    }
}
