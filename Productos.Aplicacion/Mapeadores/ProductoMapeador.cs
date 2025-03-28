using AutoMapper;
using Productos.Aplicacion.Dto;
using Productos.Dominio.Entidades;

namespace Productos.Aplicacion.Mapeadores
{
    public class ProductoMapeador: Profile
    {
        public ProductoMapeador()
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.IdProveedor, opt => opt.MapFrom(src => src.IdProveedor))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Producto, ProductoIn>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.IdProveedor, opt => opt.MapFrom(src => src.IdProveedor))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
                .ReverseMap();

            CreateMap<ProductoOut, ProductoIn>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Producto.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Producto.Descripcion))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Producto.Url))
                .ForMember(dest => dest.IdProveedor, opt => opt.MapFrom(src => src.Producto.IdProveedor))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.Producto.PrecioUnitario))
                .ReverseMap();
        }
    }
}
