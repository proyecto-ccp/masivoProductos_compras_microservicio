﻿
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Productos.Aplicacion.Comun
{
    [ExcludeFromCodeCoverage]
    public class BaseOut
    {
        public Resultado Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
