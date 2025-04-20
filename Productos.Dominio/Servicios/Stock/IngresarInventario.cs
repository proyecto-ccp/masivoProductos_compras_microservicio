
using Productos.Dominio.ObjetoValor;
using Productos.Dominio.Puertos.Integraciones;
using Productos.Dominio.Puertos.Repositorios;


namespace Productos.Dominio.Servicios.Stock
{
    public class IngresarInventario(IServicioInventariosApi servicioInventarioApi, IParametroRepositorio parametros)
    {
        private readonly IServicioInventariosApi _servicioInventarioApi = servicioInventarioApi;
        private readonly IParametroRepositorio _parametros = parametros;

        public async Task Ejecutar(IngresarStock input)
        {
            var urlBase = await DarParametro(EnumeradorParametros.inventariosUrlBase);
            var path = await DarParametro(EnumeradorParametros.inventariosIngresar);
            var uri = string.Concat(urlBase, path);
            
            await _servicioInventarioApi.Ingresar(input, uri);
            
        }

        private async Task<string> DarParametro(EnumeradorParametros parametro) 
        {
            var parametroValor = await _parametros.DarParametro(parametro.ToString()) ?? throw new  ArgumentNullException(parametro.ToString(), "El parametro no existe");

            return parametroValor.Valor;
        }
    }
}
