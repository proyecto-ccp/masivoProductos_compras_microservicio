
using AutoMapper;
using Productos.Aplicacion.Producto.Comandos;
using Productos.Dominio.Servicios.Productos;
using Productos.Aplicacion.Producto.Mapeadores;
using Moq;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Aplicacion.Comun;
using System.Net;
using Productos.Dominio.ObjetoValor;

namespace Productos.Tests.Aplicacion.Comandos
{
    public class ProductoCrearHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly RegistrarProducto _registrarProducto;
        private readonly Mock<IProductoRepositorio> mockProductoRepositorio;
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;
        public ProductoCrearHandlerTest()
        {
            mockProductoRepositorio = new Mock<IProductoRepositorio>();
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProductoMapeador()));
            _mapper = config.CreateMapper();
            _registrarProducto = new RegistrarProducto(mockProductoRepositorio.Object, mockAtributoRepositorio.Object);
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
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
                "http://example.com/foto2.jpg"
            );


            var proveedor = new Proveedor { Id = request.IdProveedor };
            var categoria = new Categoria { Id = request.IdCategoria };
            var marca = new Marca { Id = request.IdMarca };
            var modelo = new Modelo { Id = request.IdModelo };
            var color = new Color { Id = request.IdColor };
            var material = new Material { Id = request.IdMaterial };
            var medida = new Medida { Id = request.IdMedida };

            mockAtributoRepositorio.Setup(repo => repo.DarCategoria(request.IdCategoria)).ReturnsAsync(categoria);
            mockAtributoRepositorio.Setup(repo => repo.DarMarca(request.IdMarca)).ReturnsAsync(marca);
            mockAtributoRepositorio.Setup(repo => repo.DarModelo(request.IdModelo)).ReturnsAsync(modelo);
            mockAtributoRepositorio.Setup(repo => repo.DarColor(request.IdColor)).ReturnsAsync(color);
            mockAtributoRepositorio.Setup(repo => repo.DarMaterial(request.IdMaterial)).ReturnsAsync(material);
            mockAtributoRepositorio.Setup(repo => repo.DarMedida(request.IdMedida)).ReturnsAsync(medida);

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto);

            var result = await objPrueba.Handle(request, CancellationToken.None);
            
            Assert.Equal(Resultado.Exitoso, result.Resultado);
            Assert.Equal("Producto registrado correctamente", result.Mensaje);
            Assert.Equal(HttpStatusCode.OK, result.Status);
        }

        
        [Fact]
        public async Task Handle_InvalidRequest_ReturnsError()
        {
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
                "http://example.com/foto2.jpg"
            );

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto);
            var result = await objPrueba.Handle(request, CancellationToken.None);

            Assert.Equal(Resultado.Error, result.Resultado);
            Assert.Contains("Message:", result.Mensaje);
        }
    }
}
