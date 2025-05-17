
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
        private readonly ProductoCrear _peticion;
        private readonly Mock<IServicioAuditoriaApi> mockServicioAuditoriaApi;

        public ProductoCrearHandlerTest()
        {
            mockProductoRepositorio = new Mock<IProductoRepositorio>();
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProductoMapeador()));
            _mapper = config.CreateMapper();
            _registrarProducto = new RegistrarProducto(mockProductoRepositorio.Object, mockAtributoRepositorio.Object);
            _mockServicioInventarioApi = new Mock<IServicioInventariosApi>();
            _mockParametros = new Mock<IParametroRepositorio>();
            _ingresarInventario = new IngresarInventario(_mockServicioInventarioApi.Object);
            _servicioConsultar = new Consultar(mockProductoRepositorio.Object);
            mockServicioAuditoriaApi = new Mock<IServicioAuditoriaApi>();
            var baseIn = new BaseIn
            {
                Token = "tokenPruebasUnitarias",
                IdUsuario = Guid.NewGuid().ToString(),
            };
            _peticion = new ProductoCrear
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
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            var productoCreado = new Producto
            {
                Id = 100
            };

            mockProductoRepositorio.Setup(m => m.Guardar(It.IsAny<Producto>())).ReturnsAsync(productoCreado);


            var proveedor = new Proveedor { Id = (Guid)_peticion.IdProveedor };
            var categoria = new Categoria { Id = (int)_peticion.IdCategoria };
            var marca = new Marca { Id = (int)_peticion.IdMarca };
            var modelo = new Modelo { Id = (int)_peticion.IdModelo };
            var color = new Color { Id = (int)_peticion.IdColor };
            var material = new Material { Id = (int)_peticion.IdMaterial };
            var medida = new Medida { Id = (int)_peticion.IdMedida };

            mockAtributoRepositorio.Setup(repo => repo.DarCategoria((int)_peticion.IdCategoria)).ReturnsAsync(categoria);
            mockAtributoRepositorio.Setup(repo => repo.DarMarca((int)_peticion.IdMarca)).ReturnsAsync(marca);
            mockAtributoRepositorio.Setup(repo => repo.DarModelo((int)_peticion.IdModelo)).ReturnsAsync(modelo);
            mockAtributoRepositorio.Setup(repo => repo.DarColor((int)_peticion.IdColor)).ReturnsAsync(color);
            mockAtributoRepositorio.Setup(repo => repo.DarMaterial((int)_peticion.IdMaterial)).ReturnsAsync(material);
            mockAtributoRepositorio.Setup(repo => repo.DarMedida((int)_peticion.IdMedida)).ReturnsAsync(medida);

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto, _ingresarInventario, _servicioConsultar, mockServicioAuditoriaApi.Object);

            var result = await objPrueba.Handle(_peticion, CancellationToken.None);
            
            Assert.Equal(Resultado.Exitoso, result.Resultado);
            Assert.Contains("Producto registrado correctamente", result.Mensaje);
            Assert.Equal(HttpStatusCode.Created, result.Status);
        }

        
        [Fact]
        public async Task Handle_InvalidRequest_ReturnsError()
        {
            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto, _ingresarInventario, _servicioConsultar, mockServicioAuditoriaApi.Object);
            var result = await objPrueba.Handle(_peticion, CancellationToken.None);

            Assert.Equal(Resultado.Error, result.Resultado);
            Assert.Contains("Message:", result.Mensaje);
        }

        [Fact]
        public async Task Handle_AfectaInventario()
        {
            var productoCreado = new Producto
            {
                Id = 100
            };

            mockProductoRepositorio.Setup(m => m.Guardar(It.IsAny<Producto>())).ReturnsAsync(productoCreado);

            var proveedor = new Proveedor { Id = (Guid)_peticion.IdProveedor };
            var categoria = new Categoria { Id = (int)_peticion.IdCategoria };
            var marca = new Marca { Id = (int)_peticion.IdMarca };
            var modelo = new Modelo { Id = (int)_peticion.IdModelo };
            var color = new Color { Id = (int)_peticion.IdColor };
            var material = new Material { Id = (int)_peticion.IdMaterial };
            var medida = new Medida { Id = (int)_peticion.IdMedida };

            mockAtributoRepositorio.Setup(repo => repo.DarCategoria((int)_peticion.IdCategoria)).ReturnsAsync(categoria);
            mockAtributoRepositorio.Setup(repo => repo.DarMarca((int)_peticion.IdMarca)).ReturnsAsync(marca);
            mockAtributoRepositorio.Setup(repo => repo.DarModelo((int)_peticion.IdModelo)).ReturnsAsync(modelo);
            mockAtributoRepositorio.Setup(repo => repo.DarColor((int)_peticion.IdColor)).ReturnsAsync(color);
            mockAtributoRepositorio.Setup(repo => repo.DarMaterial((int)_peticion.IdMaterial)).ReturnsAsync(material);
            mockAtributoRepositorio.Setup(repo => repo.DarMedida((int)_peticion.IdMedida)).ReturnsAsync(medida);

            var parametro = new Parametro
            {
                Id = "Pruebas",
                Valor = "Pruebas"
            };
            _mockParametros.SetupSequence(m => m.DarParametro(It.IsAny<string>()))
                .ReturnsAsync(parametro)
                .ReturnsAsync(parametro);

            _mockServicioInventarioApi.Setup(m => m.Ingresar(It.IsAny<IngresarStock>(), It.IsAny<string>()));

            var objPrueba = new ProductoCrearHandler(_mapper, _registrarProducto, _ingresarInventario, _servicioConsultar, mockServicioAuditoriaApi.Object);

            var result = await objPrueba.Handle(_peticion, CancellationToken.None);

            Assert.Equal(Resultado.Exitoso, result.Resultado);
            Assert.Equal(HttpStatusCode.Created, result.Status);
        }
    }
}
