
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Comandos;
using Productos.Aplicacion.Producto.Consultas;
using Productos.Aplicacion.Producto.Dto;

namespace ServicioProducto.Api.Controllers
{
    /// <summary>
    /// Controlador de atributos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProductosController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un producto nuevo relacionado a un proveedor
        /// </summary>
        /// <response code="201"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpPost]
        [Route("Crear")]
        [ProducesResponseType(typeof(BaseOut), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Crear([FromBody] ProductoCrear producto)
        {
            var output = await _mediator.Send(producto);

            if (output.Resultado != Resultado.Error)
            {
                return Created(string.Empty, output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("Consultar")]
        [ProducesResponseType(typeof(ListaProductosOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Consultar()
        {
            var output = await _mediator.Send(new ProductosConsulta());

            if (output.Resultado == Resultado.SinRegistros)
            {
                return NoContent();
            }
            else if (output.Resultado == Resultado.Exitoso)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Lista todos los productos de un proveedor
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("ConsultarPorProveedor")]
        [ProducesResponseType(typeof(ListaProductosOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> ConsultarPorProveedor([FromQuery] ProductosPorProveedorConsulta input)
        {
            var output = await _mediator.Send(input);

            if (output.Resultado == Resultado.SinRegistros)
            {
                return NoContent();
            }
            else if (output.Resultado == Resultado.Exitoso)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene un producto por su Id
        /// </summary>
        /// <response code="200"> 
        /// ProductoOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("ConsultarPorId")]
        [ProducesResponseType(typeof(ProductoOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> ConsultarPorId([FromQuery] ProductoPorIdConsulta input)
        {
            var output = await _mediator.Send(input);

            if (output.Resultado == Resultado.SinRegistros)
            {
                return NoContent();
            }
            else if (output.Resultado == Resultado.Exitoso)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene las ubicaciones de un producto en las bodegas
        /// </summary>
        /// <response code="200"> 
        /// ListaUbicacionProductoOut: objeto de salida <br/>
        /// Resultado: Enumerador de la operación, Exitoso = 1, Error = 2, SinRegistros = 3 <br/>
        /// Mensaje: Mensaje de la operación <br/>
        /// Status: Código de estado HTTP <br/>
        /// </response>
        [HttpGet]
        [Route("{idproducto}/bodegas")]
        [ProducesResponseType(typeof(ListaUbicacionProductoOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseOut), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> ConsultarPorId([FromRoute] int idproducto)
        {
            var output = await _mediator.Send(new ProductoEnBodegaConsulta(idproducto));

            if (output.Resultado == Resultado.SinRegistros)
            {
                return NotFound(new { output.Resultado, output.Mensaje, output.Status });
            }
            else if (output.Resultado == Resultado.Exitoso)
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
