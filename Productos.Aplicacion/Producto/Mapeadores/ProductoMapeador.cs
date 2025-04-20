using AutoMapper;
using Productos.Aplicacion.Producto.Comandos;
using Productos.Aplicacion.Producto.Dto;


namespace Productos.Aplicacion.Producto.Mapeadores
{
    public class ProductoMapeador: Profile
    {
        public ProductoMapeador()
        {
            CreateMap<Dominio.Entidades.Producto, ProductoDto>().ReverseMap();

            CreateMap<ProductoCrear, Dominio.Entidades.Producto>().ReverseMap();
        }
    }
}
