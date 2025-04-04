
using Moq;
using Productos.Dominio.Entidades;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios.Productos;
using Productos.Tests.DataTests;

namespace Productos.Tests.Dominio
{
    public class RegistrarProductoTest
    {
        private readonly Mock<IProductoRepositorio> mockProductoRepositorio;
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;

        public RegistrarProductoTest()
        {
            mockProductoRepositorio = new Mock<IProductoRepositorio>();
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
        }

        /// <summary>
        /// Valida el error no existe el atributo del producto
        /// </summary>
        [Theory]
        [ClassData(typeof(RegistrarProductoDataTest))]
        public async Task Crear_ValidacionesAtributos(Categoria categoria, Marca marca, Modelo modelo, Color color, Material material, Medida medida, string msj) 
        {
            mockAtributoRepositorio.Setup(x => x.DarCategoria(It.IsAny<int>())).ReturnsAsync(categoria ?? (Categoria)null);
            mockAtributoRepositorio.Setup(x => x.DarMarca(It.IsAny<int>())).ReturnsAsync(marca ?? (Marca)null);
            mockAtributoRepositorio.Setup(x => x.DarModelo(It.IsAny<int>())).ReturnsAsync(modelo ?? (Modelo)null);
            mockAtributoRepositorio.Setup(x => x.DarColor(It.IsAny<int>())).ReturnsAsync(color ?? (Color)null);
            mockAtributoRepositorio.Setup(x => x.DarMaterial(It.IsAny<int>())).ReturnsAsync(material ?? (Material)null);
            mockAtributoRepositorio.Setup(x => x.DarMedida(It.IsAny<int>())).ReturnsAsync(medida ?? (Medida)null);

            var objPrueba = new RegistrarProducto(mockProductoRepositorio.Object, mockAtributoRepositorio.Object);

            try {
                await objPrueba.Crear(new Producto());
            }
            catch (Exception ex)
            {
                Assert.Contains(msj, ex.Message);
            }

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1000)]
        public async Task Crear_ValidaPrecio(int precio) 
        { 
            var input = new Producto()
            {
                Id = 1,
                IdCategoria = 1,
                IdMarca = 1,
                IdModelo = 1,
                IdColor = 1,
                IdMaterial = 1,
                IdMedida = 1,
                PrecioUnitario = precio,
                IdProveedor = Guid.NewGuid()   
            };

            mockAtributoRepositorio.Setup(x => x.DarCategoria(It.IsAny<int>())).ReturnsAsync(new Categoria());
            mockAtributoRepositorio.Setup(x => x.DarMarca(It.IsAny<int>())).ReturnsAsync(new Marca());
            mockAtributoRepositorio.Setup(x => x.DarModelo(It.IsAny<int>())).ReturnsAsync(new Modelo());
            mockAtributoRepositorio.Setup(x => x.DarColor(It.IsAny<int>())).ReturnsAsync(new Color());
            mockAtributoRepositorio.Setup(x => x.DarMaterial(It.IsAny<int>())).ReturnsAsync(new Material());
            mockAtributoRepositorio.Setup(x => x.DarMedida(It.IsAny<int>())).ReturnsAsync(new Medida());

            var objPrueba = new RegistrarProducto(mockProductoRepositorio.Object, mockAtributoRepositorio.Object);

            try
            {
                await objPrueba.Crear(input);
            }
            catch (Exception ex)
            {
                Assert.Contains("Valor incorrecto", ex.Message);
            }
        }

        [Fact]
        public async Task Crear_ProductoCreadoCorrectamente()
        {
            var producto = new Producto
            {
                Id = 1,
                IdProveedor = Guid.NewGuid(),
                IdCategoria = 1,
                IdMarca = 1,
                IdModelo = 1,
                IdColor = 1,
                IdMaterial = 1,
                IdMedida = 1,
                PrecioUnitario = 100
            };

            var proveedor = new Proveedor { Id = producto.IdProveedor };
            var categoria = new Categoria { Id = producto.IdCategoria };
            var marca = new Marca { Id = producto.IdMarca };
            var modelo = new Modelo { Id = producto.IdModelo };
            var color = new Color { Id = producto.IdColor };
            var material = new Material { Id = producto.IdMaterial };
            var medida = new Medida { Id = producto.IdMedida };

            mockAtributoRepositorio.Setup(repo => repo.DarCategoria(producto.IdCategoria)).ReturnsAsync(categoria);
            mockAtributoRepositorio.Setup(repo => repo.DarMarca(producto.IdMarca)).ReturnsAsync(marca);
            mockAtributoRepositorio.Setup(repo => repo.DarModelo(producto.IdModelo)).ReturnsAsync(modelo);
            mockAtributoRepositorio.Setup(repo => repo.DarColor(producto.IdColor)).ReturnsAsync(color);
            mockAtributoRepositorio.Setup(repo => repo.DarMaterial(producto.IdMaterial)).ReturnsAsync(material);
            mockAtributoRepositorio.Setup(repo => repo.DarMedida(producto.IdMedida)).ReturnsAsync(medida);

            var objPrueba = new RegistrarProducto(mockProductoRepositorio.Object, mockAtributoRepositorio.Object);

            await objPrueba.Crear(producto);

            // Assert
            mockProductoRepositorio.Verify(repo => repo.Guardar(It.Is<Producto>(p =>
                p.IdProveedor == producto.IdProveedor &&
                p.IdCategoria == producto.IdCategoria &&
                p.IdMarca == producto.IdMarca &&
                p.IdModelo == producto.IdModelo &&
                p.IdColor == producto.IdColor &&
                p.IdMaterial == producto.IdMaterial &&
                p.IdMedida == producto.IdMedida &&
                p.PrecioUnitario == producto.PrecioUnitario
            )), Times.Once);
        }
    }
}
