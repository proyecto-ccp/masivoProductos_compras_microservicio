﻿using Productos.Aplicacion.Comun;

namespace Productos.Aplicacion.Atributos.Dto
{
    public class MedidaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    public class MedidaOut : BaseOut
    {
        public MedidaDto Medida { get; set; }
    }

    public class ListaMedidasOut : BaseOut
    {
        public List<MedidaDto> Medidas { get; set; }
    }
}
