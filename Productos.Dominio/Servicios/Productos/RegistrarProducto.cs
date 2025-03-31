
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
            await Task.WhenAll(tareaCategoria,tareaMarca,tareaModelo,tareaColor,tareaMaterial,tareaMedida);
            
            input.Categoria = tareaCategoria.Result;
            input.Marca = tareaMarca.Result;
            input.Modelo = tareaModelo.Result;
            input.Material = tareaMaterial.Result;
            input.Color = tareaColor.Result;
            input.Medida = tareaMedida.Result;  
            
            ValidacionValorPrecio(input.PrecioUnitario);
            await _productoRepositorio.Guardar(input);
        }

        private Task<Categoria> EstablecerCategoria(Producto input)
        {
            return _atributoRepositorio.DarCategoria(input.Categoria.Id) ?? throw new Exception("La categoria del producto es incorrecta");
        }
        private Task<Marca> EstablecerMarca(Producto input)
        {
            return _atributoRepositorio.DarMarca(input.Marca.Id) ?? throw new Exception("La marca del producto es incorrecta");
        }   
        private Task<Modelo> EstablecerModelo(Producto input)
        {
            return _atributoRepositorio.DarModelo(input.Modelo.Id) ?? throw new Exception("El modelo del producto es incorrecto");
        }
        private Task<Color> EstablecerColor(Producto input)
        {
            return _atributoRepositorio.DarColor(input.Color.Id) ?? throw new Exception("El color del producto es incorrecto");
        }

        private Task<Material> EstablecerMaterial(Producto input)
        {
            return _atributoRepositorio.DarMaterial(input.Material.Id) ?? throw new Exception("El material del producto es incorrecto");
        }

        private Task<Medida> EstablecerMedida(Producto input)
        {
            return _atributoRepositorio.DarMedida(input.Medida.Id) ?? throw new Exception("La medida del producto es incorrecta");
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
