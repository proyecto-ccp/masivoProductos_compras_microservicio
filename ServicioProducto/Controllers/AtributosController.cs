
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Productos.Aplicacion.Atributos.Consultas;
using Productos.Aplicacion.Atributos.Dto;
using Productos.Aplicacion.Comun;


namespace ServicioProducto.Api.Controllers
{
    /// <summary>
    /// Controlador de atributos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AtributosController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public AtributosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene la lista del atributo de producto categorias
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Categorias")]
        [ProducesResponseType(typeof(ListaCategoriaOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarCategorias()
        {
            var output = await _mediator.Send(new CategoriaConsulta());

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene la lista del atributo de producto medidas
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Medidas")]
        [ProducesResponseType(typeof(ListaMedidasOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarMedidas()
        {
            var output = await _mediator.Send(new MedidaConsulta());

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene la lista del atributo de producto marcas
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Marcas")]
        [ProducesResponseType(typeof(ListaMarcaOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarMarcas()
        {
            var output = await _mediator.Send(new MarcaConsulta());

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene la lista del atributo de producto colores
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Colores")]
        [ProducesResponseType(typeof(ListaColorOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarColores()
        {
            var output = await _mediator.Send(new ColorConsulta());

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene la lista del atributo de producto modelos
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Modelos")]
        [ProducesResponseType(typeof(ListaModelosOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarModelos()
        {
            var output = await _mediator.Send(new ModeloConsulta());

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene la lista del atributo de producto materiales
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Materiales")]
        [ProducesResponseType(typeof(ListaMaterialOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarMateriales()
        {
            var output = await _mediator.Send(new MaterialConsulta());

            if (output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }
    }
}
