

using Moq;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios.Atributo;
using Productos.Tests.DataTests;

namespace Productos.Tests.Dominio
{
    public class ConsultarAtributosTest
    {
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;

        public ConsultarAtributosTest() 
        {
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
        }


        /// Valida la respuesta al consultar las categorias
        [Theory]
        [ClassData(typeof(CategoriaDataTest))]
        public async Task DarCategorias_NumeroElementos(List<Categoria> resRepositorio, int cantidad) 
        {
            mockAtributoRepositorio.Setup(m => m.DarCategorias()).ReturnsAsync(resRepositorio);

            var objPrueba = new ConsultarAtributos(mockAtributoRepositorio.Object);

            var resultado = await objPrueba.DarCategorias();

            var verResultado = Assert.IsType<List<Categoria>>(resultado);
            Assert.Equal(cantidad, verResultado.Count);

        }

        /// Valida la respuesta al consultar los colores
        [Theory]
        [ClassData(typeof(ColorDataTest))]
        public async Task DarColores_NumeroElementos(List<Color> resRepositorio, int cantidad)
        {
            mockAtributoRepositorio.Setup(m => m.DarColores()).ReturnsAsync(resRepositorio);

            var objPrueba = new ConsultarAtributos(mockAtributoRepositorio.Object);

            var resultado = await objPrueba.DarColores();

            var verResultado = Assert.IsType<List<Color>>(resultado);
            Assert.Equal(cantidad, verResultado.Count);

        }

        /// Valida la respuesta al consultar las marcas
        [Theory]
        [ClassData(typeof(MarcaDataTest))]
        public async Task DarMarcas_NumeroElementos(List<Marca> resRepositorio, int cantidad)
        {
            mockAtributoRepositorio.Setup(m => m.DarMarcas()).ReturnsAsync(resRepositorio);

            var objPrueba = new ConsultarAtributos(mockAtributoRepositorio.Object);

            var resultado = await objPrueba.DarMarcas();

            var verResultado = Assert.IsType<List<Marca>>(resultado);
            Assert.Equal(cantidad, verResultado.Count);

        }

        /// Valida la respuesta al consultar los materiales
        [Theory]
        [ClassData(typeof(MaterialDataTest))]
        public async Task DarMateriales_NumeroElementos(List<Material> resRepositorio, int cantidad)
        {
            mockAtributoRepositorio.Setup(m => m.DarMateriales()).ReturnsAsync(resRepositorio);

            var objPrueba = new ConsultarAtributos(mockAtributoRepositorio.Object);

            var resultado = await objPrueba.DarMateriales();

            var verResultado = Assert.IsType<List<Material>>(resultado);
            Assert.Equal(cantidad, verResultado.Count);

        }

        /// Valida la respuesta al consultar las medidas
        [Theory]
        [ClassData(typeof(MedidaDataTest))]
        public async Task DarMedidas_NumeroElementos(List<Medida> resRepositorio, int cantidad)
        {
            mockAtributoRepositorio.Setup(m => m.DarMedidas()).ReturnsAsync(resRepositorio);

            var objPrueba = new ConsultarAtributos(mockAtributoRepositorio.Object);

            var resultado = await objPrueba.DarMedidas();

            var verResultado = Assert.IsType<List<Medida>>(resultado);
            Assert.Equal(cantidad, verResultado.Count);

        }

        /// Valida la respuesta al consultar las medidas
        [Theory]
        [ClassData(typeof(ModeloDataTest))]
        public async Task DarModelos_NumeroElementos(List<Modelo> resRepositorio, int cantidad)
        {
            mockAtributoRepositorio.Setup(m => m.DarModelos()).ReturnsAsync(resRepositorio);

            var objPrueba = new ConsultarAtributos(mockAtributoRepositorio.Object);

            var resultado = await objPrueba.DarModelos();

            var verResultado = Assert.IsType<List<Modelo>>(resultado);
            Assert.Equal(cantidad, verResultado.Count);

        }
    }
}
