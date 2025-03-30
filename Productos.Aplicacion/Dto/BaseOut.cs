using Productos.Aplicacion.Enumeradores;
using System.Net;

namespace Productos.Aplicacion.Dto
{
    public class BaseOut
    {
        public Resultado Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
