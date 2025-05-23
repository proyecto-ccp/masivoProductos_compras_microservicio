﻿
using Microsoft.Extensions.Configuration;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Productos.Infraestructura.Adaptadores.Integraciones
{
    [ExcludeFromCodeCoverage]
    public class ServicioInventariosApi : IServicioInventariosApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ServicioInventariosApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task Ingresar(IngresarStock input, string token)
        {
            var uri = _configuration["UriInventarios"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respuesta = await _httpClient.PostAsJsonAsync(uri, input);
            respuesta.EnsureSuccessStatusCode();
        }
    }
}
