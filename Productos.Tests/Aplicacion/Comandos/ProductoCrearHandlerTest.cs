
using AutoMapper;
using Productos.Aplicacion.Producto.Comandos;
using Productos.Dominio.Servicios.Productos;
using Productos.Aplicacion.Producto.Mapeadores;
using Moq;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Aplicacion.Comun;
using System.Net;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Servicios.Stock;
using Productos.Dominio.Puertos.Integraciones;
using Consultar = Productos.Dominio.Servicios.Productos.Consultar;
using Productos.Dominio.Entidades;

namespace Productos.Tests.Aplicacion.Comandos
{
    public class ProductoCrearHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly RegistrarProducto _registrarProducto;
        private readonly Mock<IProductoRepositorio> mockProductoRepositorio;
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;
        private readonly Mock<IServicioInventariosApi> _mockServicioInventarioApi;
        private readonly Mock<IParametroRepositorio> _mockParametros;
        private readonly IngresarInventario _ingresarInventario;
        private readonly Consultar _servicioConsultar;
        
        public ProductoCrearHandlerTest()
        {
            mockProductoRepositorio = new Mock<IProductoRepositorio>();
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProductoMapeador()));
            _mapper = config.CreateMapper();
            _registrarProducto = new RegistrarProducto(mockProductoRepositorio.Object, mockAtributoRepositorio.Object);
            _mockServicioInventarioApi = new Mock<IServicioInventariosApi>();
            _mockParametros = new Mock<IParametroRepositorio>();
            _ingresarInventario = new IngresarInventario(_mockServicioInventarioApi.Object, _mockParametros.Object);
            _servicioConsultar = new Consultar(mockProductoRepositorio.Object);
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
                "http://example.com/foto2.jpg",
                100
            );

            var productoCreado = new Producto
            {
                Id = 100
            };

            mockProductoRepositorio.Setup(m => m.Guardar(It.IsAny<Producto>())).ReturnsAsync(productoCreado);


            var proveedor = new Proveedor { Id = (Guid)request.IdProveedor };
            var categoria = new Categoria { Id = (int)request.IdCategoria };
            var marca = new Marca { Id = (int)request.IdMarca };
            var modelo = new Modelo { Id = (int)request.IdModelo };
            var color = new Color { Id = (int)request.IdColor };
            var material = new Material { Id = (int)request.IdMaterial };
            var medida = new Medida { Id = (int)request.IdMedida };

            mockAtributoRepositorio.Setup(repo => repo.DarCategoria((int)request.IdCategoria)).ReturnsAsync(categoria);
            mockAtributoRepositorio.Setup(repo => repo.DarMarca((int)request.IdMarca)).ReturnsAsync(marca);
            mockAtributoRepositorio.Setup(repo => repo.DarModelo((int)request.IdModelo)).ReturnsAsync(modelo);
            mockAtributoRepositorio.Setup(repo => repo.DarColor((int)request.IdColor)).ReturnsAsync(color);
            mockAtributoRepositorio.Setup(repo => repo.DarMaterial((int)request.IdMaterial)).ReturnsAsync(material);
            mockAtributoRepositorio.Setup(repo => repo.DarMedida((int)request.IdMedida)).ReturnsAsync(medida);

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto, _ingresarInventario, _servicioConsultar);

            var result = await objPrueba.Handle(request, CancellationToken.None);
            
            Assert.Equal(Resultado.Exitoso, result.Resultado);
            Assert.Contains("Producto registrado correctamente", result.Mensaje);
            Assert.Equal(HttpStatusCode.Created, result.Status);
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
                "http://example.com/foto2.jpg",
                100
            );

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto, _ingresarInventario, _servicioConsultar);
            var result = await objPrueba.Handle(request, CancellationToken.None);

            Assert.Equal(Resultado.Error, result.Resultado);
            Assert.Contains("Message:", result.Mensaje);
        }

        [Fact]
        public async Task Handle_AfectaInventario()
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
                "http://example.com/foto2.jpg",
                100
            );

            var productoCreado = new Producto
            {
                Id = 100
            };

            mockProductoRepositorio.Setup(m => m.Guardar(It.IsAny<Producto>())).ReturnsAsync(productoCreado);

            var proveedor = new Proveedor { Id = (Guid)request.IdProveedor };
            var categoria = new Categoria { Id = (int)request.IdCategoria };
            var marca = new Marca { Id = (int)request.IdMarca };
            var modelo = new Modelo { Id = (int)request.IdModelo };
            var color = new Color { Id = (int)request.IdColor };
            var material = new Material { Id = (int)request.IdMaterial };
            var medida = new Medida { Id = (int)request.IdMedida };

            mockAtributoRepositorio.Setup(repo => repo.DarCategoria((int)request.IdCategoria)).ReturnsAsync(categoria);
            mockAtributoRepositorio.Setup(repo => repo.DarMarca((int)request.IdMarca)).ReturnsAsync(marca);
            mockAtributoRepositorio.Setup(repo => repo.DarModelo((int)request.IdModelo)).ReturnsAsync(modelo);
            mockAtributoRepositorio.Setup(repo => repo.DarColor((int)request.IdColor)).ReturnsAsync(color);
            mockAtributoRepositorio.Setup(repo => repo.DarMaterial((int)request.IdMaterial)).ReturnsAsync(material);
            mockAtributoRepositorio.Setup(repo => repo.DarMedida((int)request.IdMedida)).ReturnsAsync(medida);

            var parametro = new Parametro
            {
                Id = "Pruebas",
                Valor = "Pruebas"
            };
            _mockParametros.SetupSequence(m => m.DarParametro(It.IsAny<string>()))
                .ReturnsAsync(parametro)
                .ReturnsAsync(parametro);

            _mockServicioInventarioApi.Setup(m => m.Ingresar(It.IsAny<IngresarStock>(), It.IsAny<string>()));

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto, _ingresarInventario, _servicioConsultar);

            var result = await objPrueba.Handle(request, CancellationToken.None);

            Assert.Equal(Resultado.Exitoso, result.Resultado);
            Assert.Contains("Inventario actualizado correctamente", result.Mensaje);
            Assert.Equal(HttpStatusCode.Created, result.Status);
        }
    }
}
