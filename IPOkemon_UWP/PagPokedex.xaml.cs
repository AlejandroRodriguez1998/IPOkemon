using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPOkemon_UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PagPokedex : Page
    {
        private List<Pokemon> ListadoPokemons;
        static Pokemon pokemonSelec = new Pokemon();
        public static int indexPokemon = 0;
        public static int opcionCombate = 0;

        public PagPokedex()
        {
            this.InitializeComponent();
            ListadoPokemons = CargarContenidoXMLPokemons();
        }

        public static List<Pokemon> CargarContenidoXMLPokemons()
        {
            List<Pokemon> Listado = new List<Pokemon>();
            XDocument doc;

            if (Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride.Equals("en-US"))
            {
                doc = XDocument.Load("PokemonsUS.xml");
            }
            else
            {
                doc = XDocument.Load("Pokemons.xml");
            }

            foreach (var node in doc.Descendants("Pokemon"))
            {
                var NuevoPokemon = new Pokemon("", 0, "", "", "", "", "", null);
                Debug.WriteLine(node.Attribute("Nombre").Value);
                NuevoPokemon.Nombre = node.Attribute("Nombre").Value;
                NuevoPokemon.Numero = Convert.ToInt32(node.Attribute("Numero").Value);
                NuevoPokemon.Descripcion = node.Attribute("Descripcion").Value;
                NuevoPokemon.Tipos = node.Attribute("Tipos").Value;
                NuevoPokemon.Debilidades = node.Attribute("Debilidades").Value;
                NuevoPokemon.Habilidad = node.Attribute("Habilidad").Value;
                NuevoPokemon.Categoria = node.Attribute("Categoria").Value;
                NuevoPokemon.ImagenPokemonPrincipal = node.Attribute("ImagenPokemonPrincipal").Value;
                Listado.Add(NuevoPokemon);
            }
            return Listado;
        }

        private void lstListaPokemons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Pokemon> pokemons = (List<Pokemon>)lstListaPokemons.ItemsSource;
            lstListaPokemonsFiltrada.Visibility = Visibility.Collapsed;
            lstListaPokemons.Visibility = Visibility.Visible;

            if (lstListaPokemons.SelectedIndex >= 0)
            {
                pokemonSelec.Nombre = pokemons[lstListaPokemons.SelectedIndex].Nombre;
                indexPokemon = lstListaPokemons.SelectedIndex;
                txbNombreNumeroPokemon.Text = pokemons[lstListaPokemons.SelectedIndex].Nombre + " - Nº "
                                            + pokemons[lstListaPokemons.SelectedIndex].Numero;
                txbDescripcionPokemon.Text = pokemons[lstListaPokemons.SelectedIndex].Descripcion;
                txbTiposPokemon.Text = pokemons[lstListaPokemons.SelectedIndex].Tipos;
                txbDebilidadesPokemon.Text = pokemons[lstListaPokemons.SelectedIndex].Debilidades;
                txbHabilidadPokemon.Text = pokemons[lstListaPokemons.SelectedIndex].Habilidad;
                txbCategoriaPokemon.Text = pokemons[lstListaPokemons.SelectedIndex].Categoria;
                string ruta = "ms-appx:///" + pokemons[lstListaPokemons.SelectedIndex].ImagenPokemonPrincipal;
                imgPokemonPrincipal.Source = new BitmapImage(new Uri(ruta));
            }
            else
            {
                lstListaPokemons.SelectedIndex = 0;
            }
        }

        private async void btnCombatir_ClickAsync(object sender, RoutedEventArgs e)
        {
            ContentDialog deleteFileDialog;

            deleteFileDialog = new ContentDialog
            {
                Title = "¿Quiere combatir con " + pokemonSelec.Nombre + " ?",
                Content = "El pokemon contrincante será seleccionado aleatoriamente",
                CloseButtonText = "NO",
                PrimaryButtonText = "Combatir 1 vs CPU",
                SecondaryButtonText = "Combatir 1 vs 1",
                DefaultButton = ContentDialogButton.Primary
            };

            if (Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride.Equals("en-US"))
            {
                deleteFileDialog = new ContentDialog
                {
                    Title = "Do you want to fight with " + pokemonSelec.Nombre + " ?",
                    Content = "The opponent pokemon will be randomly selected.",
                    CloseButtonText = "NO",
                    PrimaryButtonText = "Fight 1 vs CPU",
                    SecondaryButtonText = "Fight 1 vs 1",
                    DefaultButton = ContentDialogButton.Primary
                };

            }

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                opcionCombate = 0;
                Frame aux = (Frame)this.Parent;
                aux.Navigate(typeof(PagCombate));
            } else if ((result == ContentDialogResult.Secondary))
            {
                opcionCombate = 1;
                Frame aux = (Frame)this.Parent;
                aux.Navigate(typeof(PagCombate2));
            }
            else {}
        }

        public static String pokemonCombate()
        {
            Pokemon pokemonCombate = new Pokemon();
            pokemonCombate = pokemonSelec;
            return pokemonCombate.Nombre;
        }

        private async void imgBuscar_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            String filtro = txbBusqueda.Text;

            if (filtro.Equals(""))
            {
                lstListaPokemons.Visibility = Visibility.Visible;
                lstListaPokemonsFiltrada.Visibility = Visibility.Collapsed;
                lstListaPokemons.SelectedIndex = 0;
                txbNombreNumeroPokemon.Text = ListadoPokemons[0].Nombre + " - Nº " + ListadoPokemons[0].Numero;
                txbDescripcionPokemon.Text = ListadoPokemons[0].Descripcion;
                txbTiposPokemon.Text = ListadoPokemons[0].Tipos;
                txbDebilidadesPokemon.Text = ListadoPokemons[0].Debilidades;
                txbHabilidadPokemon.Text = ListadoPokemons[0].Habilidad;
                txbCategoriaPokemon.Text = ListadoPokemons[0].Categoria;
                string ruta = "ms-appx:///" + ListadoPokemons[0].ImagenPokemonPrincipal;
                imgPokemonPrincipal.Source = new BitmapImage(new Uri(ruta));
            } else
            {
                lstListaPokemonsFiltrada.Items.Clear();
                lstListaPokemonsFiltrada.Visibility = Visibility.Collapsed;
                lstListaPokemons.Visibility = Visibility.Collapsed;

                foreach (Pokemon pokemon in ListadoPokemons)
                {
                    if (filtro.Equals(pokemon.Nombre) || filtro.Equals(pokemon.Tipos))
                    {
                        lstListaPokemonsFiltrada.Items.Add(pokemon);
                        txbNombreNumeroPokemon.Text = pokemon.Nombre + " - Nº " + pokemon.Numero;
                        txbDescripcionPokemon.Text = pokemon.Descripcion;
                        txbTiposPokemon.Text = pokemon.Tipos;
                        txbDebilidadesPokemon.Text = pokemon.Debilidades;
                        txbHabilidadPokemon.Text = pokemon.Habilidad;
                        txbCategoriaPokemon.Text = pokemon.Categoria;
                        string ruta = "ms-appx:///" + pokemon.ImagenPokemonPrincipal;
                        imgPokemonPrincipal.Source = new BitmapImage(new Uri(ruta));
                    }
                }
                if(lstListaPokemonsFiltrada.Items.Count == 0)
                {
                    ContentDialog deleteFileDialog;

                    deleteFileDialog = new ContentDialog
                    {
                        Title = "LO SENTIMOS :(",
                        Content = "No se ha encontrado ningún pokemon con ese nombre.",
                        CloseButtonText = "OK",
                        DefaultButton = ContentDialogButton.Primary
                    };

                    if (Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride.Equals("en-US"))
                    {
                        deleteFileDialog = new ContentDialog
                        {
                            Title = "WE ARE SORRY :(",
                            Content = "No pokemon with that name has been found.",
                            CloseButtonText = "OK",
                            DefaultButton = ContentDialogButton.Primary
                        };
                    }

                    ContentDialogResult result = await deleteFileDialog.ShowAsync();
                    lstListaPokemonsFiltrada.Visibility = Visibility.Collapsed;
                    lstListaPokemons.Visibility = Visibility.Visible;
                    txbBusqueda.Text = "";
                } else
                {
                    lstListaPokemonsFiltrada.Visibility = Visibility.Visible;
                    txbBusqueda.Text = "";
                }
            }
        }

        private void imgBuscar_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void imgBuscar_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void btnCombatir_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            btnCombatir.Background = new SolidColorBrush(Colors.Gray);
            btnCombatir.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void btnCombatir_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            btnCombatir.Background = new SolidColorBrush(Colors.LightGray);
            btnCombatir.BorderBrush = null;
        }
    }
}
