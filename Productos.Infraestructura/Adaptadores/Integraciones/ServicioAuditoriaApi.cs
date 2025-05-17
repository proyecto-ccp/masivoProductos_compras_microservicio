﻿
using Microsoft.Extensions.Configuration;
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace Productos.Infraestructura.Adaptadores.Integraciones
{
    [ExcludeFromCodeCoverage]
    public class ServicioAuditoriaApi : IServicioAuditoriaApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ServicioAuditoriaApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task RegistrarAuditoria(Auditoria auditoria)
        {
            var uri = _configuration["UriAuditoria"];
            var respuesta = await _httpClient.PostAsJsonAsync(uri, auditoria);
            respuesta.EnsureSuccessStatusCode();
        }
    }
}
