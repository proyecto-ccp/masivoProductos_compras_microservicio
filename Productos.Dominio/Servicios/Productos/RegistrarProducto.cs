
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Entidades;
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Servicios.Productos
{
    public class RegistrarProducto(IProductoRepositorio productoRepositorio, IAtributoRepositorio atributoRepositorio)
    {
        private readonly IProductoRepositorio _productoRepositorio = productoRepositorio;
        private readonly IAtributoRepositorio _atributoRepositorio = atributoRepositorio;

        public async Task Crear(Producto input)
        {
            var tareaCategoria = EstablecerCategoria(input);
            var tareaMarca = EstablecerMarca(input);
            var tareaModelo = EstablecerModelo(input);
            var tareaColor = EstablecerColor(input);
            var tareaMaterial = EstablecerMaterial(input);
            var tareaMedida = EstablecerMedida(input);
            var tareaProveedor = EstablecerProveedor(input);
            await Task.WhenAll(tareaCategoria,tareaMarca,tareaModelo,tareaColor,tareaMaterial,tareaMedida, tareaProveedor);

            var proveedor = tareaProveedor.Result ?? throw new Exception("el proveedor es incorrecto");
            var categoria = tareaCategoria.Result ?? throw new Exception("La categoria del producto es incorrecta");
            var marca = tareaMarca.Result ?? throw new Exception("La marca del producto es incorrecta");
            var modelo = tareaModelo.Result ?? throw new Exception("El modelo del producto es incorrecto");
            var material = tareaMaterial.Result ?? throw new Exception("El material del producto es incorrecto");
            var color = tareaColor.Result ?? throw new Exception("El color del producto es incorrecto");
            var medida = tareaMedida.Result ?? throw new Exception("La medida del producto es incorrecta");

            input.IdProveedor = proveedor.Id;
            input.IdCategoria = categoria.Id;
            input.IdMarca = marca.Id;
            input.IdModelo = modelo.Id;
            input.IdMaterial = material.Id;
            input.IdColor = color.Id;   
            input.IdMedida = medida.Id;

            ValidacionValorPrecio(input.PrecioUnitario);
            await _productoRepositorio.Guardar(input);
        }

        private Task<Categoria> EstablecerCategoria(Producto input)
        {
            return _atributoRepositorio.DarCategoria(input.IdCategoria);
        }
        private Task<Marca> EstablecerMarca(Producto input)
        {
            return _atributoRepositorio.DarMarca(input.IdMarca);
        }   
        private Task<Modelo> EstablecerModelo(Producto input)
        {
            return _atributoRepositorio.DarModelo(input.IdModelo);
        }
        private Task<Color> EstablecerColor(Producto input)
        {
            return _atributoRepositorio.DarColor(input.IdColor);
        }

        private Task<Material> EstablecerMaterial(Producto input)
        {
            return _atributoRepositorio.DarMaterial(input.IdMaterial);
        }

        private Task<Medida> EstablecerMedida(Producto input)
        {
            return _atributoRepositorio.DarMedida(input.IdMedida);
        }

        private Task<Proveedor> EstablecerProveedor(Producto input) 
        {
            //Pendiente consumir el servicio de proveedores para validar que el proveedor exista
            var proveedor = new Proveedor
            {
                Id = input.IdProveedor,
                Nombre = "NombreProveedor",
                Direccion = "DireccionProveedor",
                IdCiudad = 1,
                Telefono = "TelefonoProveedor",
                Correo = "CorreoProveedor",
                IdTributario = "IdTributarioProveedor",
                IdPostal = "IdPostalProveedor",
                descripcion = "DescripcionProveedor"
            };

            return Task.FromResult(proveedor);
        }

        private static void ValidacionValorPrecio(decimal precio) 
        {
            if (precio <= 0)
            {
                throw new Exception("El precio del producto debe ser mayor a 0");
            }
        }



    }
}
