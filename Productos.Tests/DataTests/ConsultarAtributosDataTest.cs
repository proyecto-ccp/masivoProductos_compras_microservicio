
using Productos.Dominio.ObjetoValor;

namespace Productos.Tests.DataTests
{
    public class CategoriaDataTest : TheoryData<List<Categoria>, int>
    {
        public CategoriaDataTest() 
        {
            List<Categoria> existeRegistros = 
                [
                    new Categoria { Id = 1, Codigo = "Prueba", Nombre = "Pruebas" },
                    new Categoria { Id = 2, Codigo = "Prueba2", Nombre = "Pruebas2" }
                ];

            List<Categoria> NoExisteRegistros = [];

            Add(existeRegistros, 2);
            Add(NoExisteRegistros, 0);
            Add(null, 0);
        }

    }

    public class ColorDataTest : TheoryData<List<Color>, int>
    {
        public ColorDataTest()
        {
            List<Color> existeRegistros =
                [
                    new Color { Id = 1, Codigo = "Prueba", Nombre = "Pruebas" },
                    new Color { Id = 2, Codigo = "Prueba2", Nombre = "Pruebas2" }
                ];

            List<Color> NoExisteRegistros = [];

            Add(existeRegistros, 2);
            Add(NoExisteRegistros, 0);
            Add(null, 0);
        }

    }

    public class MarcaDataTest : TheoryData<List<Marca>, int>
    {
        public MarcaDataTest()
        {
            List<Marca> existeRegistros =
                [
                    new Marca { Id = 1, Codigo = "Prueba", Nombre = "Pruebas" },
                    new Marca { Id = 2, Codigo = "Prueba2", Nombre = "Pruebas2" }
                ];

            List<Marca> NoExisteRegistros = [];

            Add(existeRegistros, 2);
            Add(NoExisteRegistros, 0);
            Add(null, 0);
        }

    }

    public class MaterialDataTest : TheoryData<List<Material>, int>
    {
        public MaterialDataTest()
        {
            List<Material> existeRegistros =
                [
                    new Material { Id = 1, Codigo = "Prueba", Nombre = "Pruebas" },
                    new Material { Id = 2, Codigo = "Prueba2", Nombre = "Pruebas2" }
                ];

            List<Material> NoExisteRegistros = [];

            Add(existeRegistros, 2);
            Add(NoExisteRegistros, 0);
            Add(null, 0);
        }

    }

    public class MedidaDataTest : TheoryData<List<Medida>, int>
    {
        public MedidaDataTest()
        {
            List<Medida> existeRegistros =
                [
                    new Medida { Id = 1, Codigo = "Prueba", Nombre = "Pruebas" },
                    new Medida { Id = 2, Codigo = "Prueba2", Nombre = "Pruebas2" }
                ];

            List<Medida> NoExisteRegistros = [];

            Add(existeRegistros, 2);
            Add(NoExisteRegistros, 0);
            Add(null, 0);
        }

    }

    public class ModeloDataTest : TheoryData<List<Modelo>, int>
    {
        public ModeloDataTest()
        {
            List<Modelo> existeRegistros =
                [
                    new Modelo { Id = 1, Codigo = "Prueba", Nombre = "Pruebas" },
                    new Modelo { Id = 2, Codigo = "Prueba2", Nombre = "Pruebas2" }
                ];

            List<Modelo> NoExisteRegistros = [];

            Add(existeRegistros, 2);
            Add(NoExisteRegistros, 0);
            Add(null, 0);
        }

    }
}
