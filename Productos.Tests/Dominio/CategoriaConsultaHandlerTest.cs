
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Productos.Aplicacion.Atributos.Consultas;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Aplicacion.Atributos.Mapeadores;
using Productos.Aplicacion.Comun;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Servicios.Atributo;
using System.Net;

namespace Productos.Tests.Dominio
{
    public class CategoriaConsultaHandlerTest
    {
        private readonly ConsultarAtributos _servicioAtributos;
        private readonly IMapper _mapper;
        private readonly Mock<IAtributoRepositorio> mockAtributoRepositorio;

        public CategoriaConsultaHandlerTest()
        {
            mockAtributoRepositorio = new Mock<IAtributoRepositorio>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AtributoMapeador()));
            _mapper = config.CreateMapper();
            _servicioAtributos = new ConsultarAtributos(mockAtributoRepositorio.Object);
        }

        /// <summary>
        /// Valida las posibles respuestas de la consulta de categorias.
        /// </summary>
        [Theory]
        [InlineData(1, Resultado.Exitoso, HttpStatusCode.OK, "Consulta exitosa", 2)]
        [InlineData(2, Resultado.SinRegistros, HttpStatusCode.NoContent, "El atributo categorias no tiene valores", 0)]
        [InlineData(3, Resultado.Error, HttpStatusCode.InternalServerError, "Error", 0)]
        public async Task Handle_Categoria_Respuestas(int tipo, Resultado res, HttpStatusCode status, string msj, int cantidad)
        {
            List<Categoria> categorias = [];

            if (tipo == 1) 
            {
                categorias =
                [
                    new() { Id = 1, Nombre = "Categoria 1", Codigo = "CO1" },
                    new() { Id = 2, Nombre = "Categoria 2", Codigo = "CO2" },
                ];
            }

            if (tipo == 3)
            {
                mockAtributoRepositorio.Setup(repo => repo.DarCategorias()).ThrowsAsync(new Exception("Error"));
            }
            else 
            {
                mockAtributoRepositorio.Setup(repo => repo.DarCategorias()).ReturnsAsync(categorias);
            }

            var objPrueba = new CategoriaConsultaHandler(_servicioAtributos, _mapper);
            var resultado = await objPrueba.Handle(new CategoriaConsulta(), CancellationToken.None);

            var verResultado = Assert.IsType<ListaCategoriaOut>(resultado);
            Assert.Equal(cantidad, verResultado.Categorias.Count);
            Assert.Equal(res, verResultado.Resultado);
            Assert.Equal(status, verResultado.Status);
            Assert.Equal(msj, verResultado.Mensaje);

        }
    }
}
