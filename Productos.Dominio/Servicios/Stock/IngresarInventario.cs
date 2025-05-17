
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;

namespace Productos.Dominio.Servicios.Stock
{
    public class IngresarInventario(IServicioInventariosApi servicioInventarioApi)
    {
        private readonly IServicioInventariosApi _servicioInventarioApi = servicioInventarioApi;
        

        public async Task Ejecutar(IngresarStock input, string token)
        {

            await _servicioInventarioApi.Ingresar(input, token);
            
        }


    }
}
