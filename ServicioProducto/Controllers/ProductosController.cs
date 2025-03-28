using Microsoft.AspNetCore.Mvc;
using Productos.Aplicacion.Comandos;
using Productos.Aplicacion.Consultas;
using Productos.Aplicacion.Dto;
using Productos.Aplicacion.Enum;

namespace ServicioProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProductosController: ControllerBase
    {
        private readonly IConsultasProducto _consultasProducto;
        private readonly IComandosProducto _comandosProducto;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductosController(IConsultasProducto consultasProducto, IComandosProducto comandosProducto)
        {
            _consultasProducto = consultasProducto;
            _comandosProducto = comandosProducto;
        }

        /// <summary>
        /// Obtiene la lista de productos
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet]
        [Route("Listar")]
        [ProducesResponseType(typeof(ListaProductosOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ListarProductos() 
        {
            var output = await _consultasProducto.ObtenerProductos();

            if(output.Resultado != Resultado.Error)
            {
                return Ok(output);
            }
            else
            {
                return Problem(output.Mensaje, statusCode: (int)output.Status);
            }
        }

        /// <summary>
        /// Obtiene la lista de productos
        /// </summary>
        /// <response code="200"> 
        /// ListaProductosOut pendiente
        /// </response>
        [HttpGet("{id}")]
        [Route("Listar")]
        [ProducesResponseType(typeof(ListaProductosOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerProducto(Guid id)
        {
            var output = await _consultasProducto.ObtenerProducto(id);

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
        /// Crear un producto
        /// </summary>
        /// /// <param name="input">
        /// pendiente    
        /// </param>
        /// <response code="200"> 
        /// ProductoOut
        /// </response>
        [HttpPost]
        [Route("Crear")]
        [ProducesResponseType(typeof(ProductoOut), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoIn input)
        {
            var output = await _comandosProducto.CrearProducto(input);
            
            if(output.Resultado != Resultado.Error)
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
