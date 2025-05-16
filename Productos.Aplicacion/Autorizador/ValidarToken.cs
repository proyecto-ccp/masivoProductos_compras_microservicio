

using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;

namespace Productos.Aplicacion.Autorizador
{
    public class ValidarToken (IServicioUsuariosApi servicioUsuarios)
    {
        private readonly IServicioUsuariosApi _servicioUsuarios = servicioUsuarios;

        public async Task<TokenInfo> Ejecutar(string token, string uri)
        {
            TokenInfo tokenInfo;
            try
            {
                tokenInfo = await _servicioUsuarios.ValidarToken(token);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al validar el token", ex);
            }
            return tokenInfo;
        }
    }

}
