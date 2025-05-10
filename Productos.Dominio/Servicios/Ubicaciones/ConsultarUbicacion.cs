
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Repositorios;

namespace Productos.Dominio.Servicios.Ubicaciones
{
    public class ConsultarUbicacion(IUbicacionRespositorio ubicacionRespositorio)
    {
        private readonly IUbicacionRespositorio _ubicacionRespositorio = ubicacionRespositorio;
    
        
        public Task<List<Ubicacion>> Ejecutar(int idproducto)
        {
            return _ubicacionRespositorio.ObtenerUbicacionPorIdProducto(idproducto);
        }

    }
}
