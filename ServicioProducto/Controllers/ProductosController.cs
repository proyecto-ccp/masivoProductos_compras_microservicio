using MediatR;
using Microsoft.AspNetCore.Mvc;
using Productos.Aplicacion.Comun;
using Productos.Aplicacion.Producto.Comandos;

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
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
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
    }
}
