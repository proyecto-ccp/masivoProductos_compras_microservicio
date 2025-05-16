
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Integraciones
{
    public interface IServicioUsuariosApi
    {
        Task<TokenInfo> ValidarToken(string token);
    }
}
