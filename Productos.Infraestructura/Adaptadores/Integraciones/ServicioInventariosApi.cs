
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace Productos.Infraestructura.Adaptadores.Integraciones
{
    [ExcludeFromCodeCoverage]
    public class ServicioInventariosApi : IServicioInventariosApi
    {
        private readonly HttpClient _httpClient;

        public ServicioInventariosApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Ingresar(IngresarStock input, string uri)
        {
            var respuesta = await _httpClient.PostAsJsonAsync(uri, input);
            respuesta.EnsureSuccessStatusCode();
            
        }
    }
}
