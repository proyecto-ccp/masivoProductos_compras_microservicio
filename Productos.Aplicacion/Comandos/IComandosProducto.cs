using Productos.Aplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productos.Aplicacion.Comandos
{
    public interface IComandosProducto
    {
        Task<BaseOut> CrearProducto(ProductoIn producto);
    }
}
