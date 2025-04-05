
using AutoMapper;
using Moq;
using Productos.Aplicacion.Atributos.Consultas;
using Productos.Aplicacion.Atributos.Mapeadores;
using Productos.Aplicacion.Comun;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios.Atributo;
using System.Net;

namespace Productos.Tests.Aplicacion.Consultas
{
    public class MaterialConsultaHandlerTest
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;

        public MaterialConsultaHandlerTest()
        {
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AtributoMapeador()));
            _mapper = config.CreateMapper();
            _servicioAtributos = new ConsultarAtributos(mockAtributoRepositorio.Object);
        }

        /// <summary>
        /// Valida las posibles respuestas de la consulta de materiales
        /// </summary>
        [Theory]
        [InlineData(1, Resultado.Exitoso, HttpStatusCode.OK, "Consulta exitosa", 2)]
        [InlineData(2, Resultado.SinRegistros, HttpStatusCode.NoContent, "El atributo materiales no tiene valores", 0)]
        [InlineData(3, Resultado.Error, HttpStatusCode.InternalServerError, "Error", 0)]
        public async Task Handle_Material_Respuestas(int tipo, Resultado res, HttpStatusCode status, string msj, int cantidad)
        {
            List<Material> materiales = [];
            if (tipo == 1)
            {
                materiales =
                [
                    new() { Id = 1, Nombre = "Color 1", Codigo = "CO1" },
                    new() { Id = 2, Nombre = "Color 2", Codigo = "CO2" },
                ];
            }
            if (tipo == 3)
            {
                mockAtributoRepositorio.Setup(repo => repo.DarMateriales()).ThrowsAsync(new Exception("Error"));
            }
            else
            {
                mockAtributoRepositorio.Setup(repo => repo.DarMateriales()).ReturnsAsync(materiales);
            }
            var objPrueba = new MaterialConsultaHandler(_servicioAtributos, _mapper);
            var resultado = await objPrueba.Handle(new MaterialConsulta(), CancellationToken.None);
            Assert.Equal(res, resultado.Resultado);
            Assert.Equal(status, resultado.Status);
            Assert.Equal(msj, resultado.Mensaje);
            Assert.Equal(cantidad, resultado.Materiales.Count);
        }
    }
}
