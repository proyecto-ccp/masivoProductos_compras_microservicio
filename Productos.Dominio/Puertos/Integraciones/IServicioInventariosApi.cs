

using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Puertos.Integraciones
{
    public interface IServicioInventariosApi
    {
        Task Ingresar(IngresarStock input, string uri);

    }
}
