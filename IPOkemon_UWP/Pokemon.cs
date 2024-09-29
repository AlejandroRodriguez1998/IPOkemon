using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace IPOkemon_UWP
{
    public class Pokemon : Object
    {
        public string Nombre { set; get; }
        public int Numero { set; get; }
        public string Descripcion { set; get; }
        public string Tipos { set; get; }
        public string Debilidades { set; get; }
        public string Habilidad { set; get; }
        public string Categoria { set; get; }
        public string ImagenPokemonPrincipal { set; get; }

        public Pokemon() {}

        public Pokemon(string nombre, int numero, string descripcion, string tipos, string debilidades, 
            string habilidad, string categoria, string imagenPokemonPrincipal)
        {
            Nombre = nombre;
            Numero = numero;
            Descripcion = descripcion;
            Tipos = tipos;
            Debilidades = debilidades;
            Habilidad = habilidad;
            Categoria = categoria;
            ImagenPokemonPrincipal = imagenPokemonPrincipal;
        }
    }
}
