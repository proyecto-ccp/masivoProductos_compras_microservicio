
using Productos.Dominio.ObjetoValor;

namespace Productos.Tests.DataTests
{
    public class RegistrarProductoDataTest : TheoryData<Categoria, Marca, Modelo, Color, Material, Medida, string>
    {
        public RegistrarProductoDataTest()
        {

            var existeCategoria = new Categoria
            {
                Id = 1,
                Nombre = "Categoria 1",
                Codigo = "Descripcion de la categoria 1"
            };

            var existeMarca = new Marca
            {
                Id = 1,
                Nombre = "Marca 1",
                Codigo = "Descripcion de la marca 1"
            };

            var existeModelo = new Modelo
            {
                Id = 1,
                Nombre = "Modelo 1",
                Codigo = "Descripcion del modelo 1"
            };

            var existeColor = new Color
            {
                Id = 1,
                Nombre = "Color 1",
                Codigo = "Descripcion del color 1"
            };

            var existeMaterial = new Material
            {
                Id = 1,
                Nombre = "Material 1",
                Codigo = "Descripcion del material 1"
            };

            var existeMedida = new Medida
            {
                Id = 1,
                Nombre = "Medida 1",
                Codigo = "Descripcion de la medida 1"
            };

                
            Add(null, existeMarca, existeModelo, existeColor, existeMaterial, existeMedida, "La categoria no existe");
            Add(existeCategoria, null, existeModelo, existeColor, existeMaterial, existeMedida, "La marca no existe");
            Add(existeCategoria, existeMarca, null, existeColor, existeMaterial, existeMedida, "El modelo no existe");
            Add(existeCategoria, existeMarca, existeModelo, null, existeMaterial, existeMedida, "El color no existe");
            Add(existeCategoria, existeMarca, existeModelo, existeColor, null, existeMedida, "El material no existe");
            Add(existeCategoria, existeMarca, existeModelo, existeColor, existeMaterial, null, "La medida no existe");
        }
        
    }
}
