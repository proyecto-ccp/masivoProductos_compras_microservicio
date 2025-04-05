
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
    public class MedidaConsultaHandlerTest
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;

        public MedidaConsultaHandlerTest()
        {
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AtributoMapeador()));
            _mapper = config.CreateMapper();
            _servicioAtributos = new ConsultarAtributos(mockAtributoRepositorio.Object);
        }

        /// <summary>
        /// Valida las posibles respuestas de la consulta de medidas
        /// </summary>
        [Theory]
        [InlineData(1, Resultado.Exitoso, HttpStatusCode.OK, "Consulta exitosa", 2)]
        [InlineData(2, Resultado.SinRegistros, HttpStatusCode.NoContent, "El atributo medidas no tiene valores", 0)]
        [InlineData(3, Resultado.Error, HttpStatusCode.InternalServerError, "Error", 0)]
        public async Task Handle_Medidas_Respuestas(int tipo, Resultado res, HttpStatusCode status, string msj, int cantidad)
        {
            List<Medida> medidas = [];
            if (tipo == 1)
            {
                medidas =
                [
                    new() { Id = 1, Nombre = "Color 1", Codigo = "CO1" },
                    new() { Id = 2, Nombre = "Color 2", Codigo = "CO2" },
                ];
            }
            if (tipo == 3)
            {
                mockAtributoRepositorio.Setup(repo => repo.DarMedidas()).ThrowsAsync(new Exception("Error"));
            }
            else
            {
                mockAtributoRepositorio.Setup(repo => repo.DarMedidas()).ReturnsAsync(medidas);
            }
            var objPrueba = new MedidaConsultaHandler(_servicioAtributos, _mapper);
            var resultado = await objPrueba.Handle(new MedidaConsulta(), CancellationToken.None);
            Assert.Equal(res, resultado.Resultado);
            Assert.Equal(status, resultado.Status);
            Assert.Equal(msj, resultado.Mensaje);
            Assert.Equal(cantidad, resultado.Medidas.Count);
        }

    }
}
