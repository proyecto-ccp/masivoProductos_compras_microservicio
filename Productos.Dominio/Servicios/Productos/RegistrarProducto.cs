
using Productos.Dominio.Puertos.Repositorios;
using Productos.Dominio.Entidades;
using Productos.Dominio.ObjetoValor;

namespace Productos.Dominio.Servicios.Productos
{
    public class RegistrarProducto(IProductoRepositorio productoRepositorio, IAtributoRepositorio atributoRepositorio)
    {
        private readonly IProductoRepositorio _productoRepositorio = productoRepositorio;
        private readonly IAtributoRepositorio _atributoRepositorio = atributoRepositorio;

        private readonly string _paramErrorAtributo = "Atributo";
        private readonly string _paramErrorPrecio = "Precio";

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

            var proveedor = tareaProveedor.Result;
            var categoria = tareaCategoria.Result;
            var marca = tareaMarca.Result;
            var modelo = tareaModelo.Result;
            var material = tareaMaterial.Result;
            var color = tareaColor.Result;
            var medida = tareaMedida.Result;

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

        private async Task<Categoria> EstablecerCategoria(Producto input)
        {
            Categoria categoria = await _atributoRepositorio.DarCategoria(input.IdCategoria) ?? throw new ArgumentNullException(_paramErrorAtributo, "La categoria no existe");
            return categoria;
        }
        private async Task<Marca> EstablecerMarca(Producto input)
        {
            Marca marca = await _atributoRepositorio.DarMarca(input.IdMarca) ?? throw new ArgumentNullException(_paramErrorAtributo, "La marca no existe");
            return marca;
        }   
        private async Task<Modelo> EstablecerModelo(Producto input)
        {
            Modelo modelo = await _atributoRepositorio.DarModelo(input.IdModelo) ?? throw new ArgumentNullException(_paramErrorAtributo, "El modelo no existe");
            return modelo;
        }
        private async Task<Color> EstablecerColor(Producto input)
        {
            Color color = await _atributoRepositorio.DarColor(input.IdColor) ?? throw new ArgumentNullException(_paramErrorAtributo, "El color no existe");
            return color;
        }

        private async Task<Material> EstablecerMaterial(Producto input)
        {
            Material material = await _atributoRepositorio.DarMaterial(input.IdMaterial) ?? throw new ArgumentNullException(_paramErrorAtributo, "El material no existe");
            return material;
        }

        private async Task<Medida> EstablecerMedida(Producto input)
        {
            Medida medida = await _atributoRepositorio.DarMedida(input.IdMedida) ?? throw new ArgumentNullException(_paramErrorAtributo, "La medida no existe");
            return medida;
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

        private void ValidacionValorPrecio(decimal precio) 
        {
            if (precio <= 0)
            {
                throw new ArgumentNullException(_paramErrorPrecio, "Valor incorrecto");
            }
        }



    }
}
