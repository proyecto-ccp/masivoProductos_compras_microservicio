﻿
namespace Productos.Dominio.ObjetoValor
{
    public class Proveedor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int IdCiudad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }  
        public string IdTributario { get; set; }
        public string IdPostal { get; set; }
        public string descripcion { get; set; }
    }
}
