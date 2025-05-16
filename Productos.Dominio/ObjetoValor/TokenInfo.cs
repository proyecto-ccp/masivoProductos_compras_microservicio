

using System.Net;

namespace Productos.Dominio.ObjetoValor
{
    public class TokenInfo
    {
        public string Username { get; set; }
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
