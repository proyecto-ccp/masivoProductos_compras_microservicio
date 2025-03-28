using Productos.Dominio.Entidades;

using Productos.Dominio.Puertos.Repositorios;

namespace Productos.Dominio.Servicios
{
    public class CrearProducto(IProductoRepositorio productoRepositorio)
    {
        private readonly IProductoRepositorio _productoRepositorio = productoRepositorio;

        public async Task Ejecutar(Producto producto)
        {
            if(ValidarProducto(producto))
            {
                producto.Id = Guid.NewGuid();
                producto.FechaCreacion = DateTime.Now;
                await _productoRepositorio.Guardar(producto);
            }
            else 
            {
                throw new InvalidOperationException("Valores incorrectos para los parametros minimos");
            }
        }

        public bool ValidarProducto(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrEmpty(producto.Descripcion) || producto.PrecioUnitario == 0)
                return false;
            else
                return true;
        }
    }
}
