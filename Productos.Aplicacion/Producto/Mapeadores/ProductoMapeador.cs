using AutoMapper;
using Productos.Aplicacion.Producto.Comandos;
using Productos.Aplicacion.Producto.Dto;
using Productos.Dominio.ObjetoValor;


namespace Productos.Aplicacion.Producto.Mapeadores
{
    public class ProductoMapeador: Profile
    {
        public ProductoMapeador()
        {
            CreateMap<Dominio.Entidades.Producto, ProductoDto>().ReverseMap();

            CreateMap<ProductoCrear, Dominio.Entidades.Producto>().ReverseMap();

            CreateMap<ProductoCrear, Auditoria>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.Control.IdUsuario))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "Producto creado"))
                .ForMember(dest => dest.TablaAfectada, opt => opt.MapFrom(src => "tbl_productos"));
        }
    }
}
