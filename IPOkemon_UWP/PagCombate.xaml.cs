using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPOkemon_UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PagCombate : Page
    {

        public static Frame FrameGO;
        public static String frameCont1;
        public static String frameCont2;
        static DispatcherTimer reloj = new DispatcherTimer();

        Random r = new Random();

        public PagCombate()
        {
            if (reloj.IsEnabled)
            {
                reloj.Stop();
            }

            this.InitializeComponent();

            string pokemonSeleccionado = PagPokedex.pokemonCombate();
            txbPokemonCombate1.Text = pokemonSeleccionado;
            switch (pokemonSeleccionado)
            {
                case "Charmander":
                    Frame1.Navigate(typeof(CombateCharmander));
                    frameCont1 = "Charmander";
                    break;
                case "Swablu":
                    Frame1.Navigate(typeof(CombateSwablu));
                    frameCont1 = "Swablu";
                    break;
                case "Electrode":
                    Frame1.Navigate(typeof(CombateElectrode));
                    frameCont1 = "Electrode";
                    break;
            }

            if (PagPokedex.opcionCombate == 0)
            {
                seleccionarPokemon();
            } else
            {
                txbPokemonCombate2.Text = PagCombate2.pokemonContrincante;
                switch (PagCombate2.pokemonContrincante)
                {
                    case "Charmander":
                        Frame2.Navigate(typeof(CombateCharmander));
                        frameCont2 = "Charmander";
                        break;
                    case "Swablu":
                        Frame2.Navigate(typeof(CombateSwablu));
                        frameCont2 = "Swablu";
                        break;
                    case "Electrode":
                        Frame2.Navigate(typeof(CombateElectrode));
                        frameCont2 = "Electrode";
                        break;
                }
            }

            if (PagPokedex.opcionCombate == 0)
            {
                int num = r.Next(3, 6);
                reloj.Interval = TimeSpan.FromSeconds(num);
                reloj.Tick += metodoCPU;
                reloj.Start();
            }
        }

        public void seleccionarPokemon() {
            
            List<Pokemon> lista = PagPokedex.CargarContenidoXMLPokemons();
            Pokemon pokemon2 = new Pokemon();

            int ale = r.Next(0, lista.Count);

            while(PagPokedex.indexPokemon == ale)
            {
                ale = r.Next(0, lista.Count);
            }

            pokemon2 = lista.ElementAt(ale);

            txbPokemonCombate2.Text = pokemon2.Nombre;
            switch (pokemon2.Nombre)
            {
                case "Charmander":
                    Frame2.Navigate(typeof(CombateCharmander));
                    frameCont2 = "Charmander";
                    break;
                case "Swablu":
                    Frame2.Navigate(typeof(CombateSwablu));
                    frameCont2 = "Swablu";
                    break;
                case "Electrode":
                    Frame2.Navigate(typeof(CombateElectrode));
                    frameCont2 = "Electrode";
                    break;
            }

        }

        private void Frame1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if(PagPokedex.opcionCombate != 0)
            {
                if (frameCont1.Equals("Charmander") && frameCont2.Equals("Swablu"))
                {
                    UserControl_Charmander.cancelar("");
                    UserControl_Swablu.atacar2();
                }

                if (frameCont1.Equals("Charmander") && frameCont2.Equals("Electrode"))
                {
                    UserControl_Charmander.cancelar("");
                    UserControl_Electrode.atacar2();
                }

                if (frameCont1.Equals("Swablu") && frameCont2.Equals("Charmander"))
                {
                    UserControl_Swablu.cancelar("");
                    //UserControl_Charmander.atacar2();
                }

                if (frameCont1.Equals("Swablu") && frameCont2.Equals("Electrode"))
                {
                    UserControl_Swablu.cancelar("");
                    UserControl_Electrode.atacar2();
                }

                if (frameCont1.Equals("Electrode") && frameCont2.Equals("Charmander"))
                {
                    UserControl_Electrode.cancelar("");
                    //UserControl_Charmander.atacar2();
                }

                if (frameCont1.Equals("Electrode") && frameCont2.Equals("Swablu"))
                {
                    UserControl_Electrode.cancelar("");
                    UserControl_Swablu.atacar2();
                }
            }
        }

        private void Frame2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (frameCont1.Equals("Charmander") && frameCont2.Equals("Swablu"))
            {
                UserControl_Swablu.cancelar("");
                //UserControl_Charmander.atacar2();
            }

            if (frameCont1.Equals("Charmander") && frameCont2.Equals("Electrode"))
            {
                UserControl_Electrode.cancelar("");
                //UserControl_Charmander.atacar2();
            }

            if (frameCont1.Equals("Swablu") && frameCont2.Equals("Charmander"))
            {
                UserControl_Charmander.cancelar("");
                UserControl_Swablu.atacar2();
            }

            if (frameCont1.Equals("Swablu") && frameCont2.Equals("Electrode"))
            {
                UserControl_Electrode.cancelar("");
                UserControl_Swablu.atacar2();
            }

            if (frameCont1.Equals("Electrode") && frameCont2.Equals("Charmander"))
            {
                UserControl_Charmander.cancelar("");
                UserControl_Electrode.atacar2();
            }

            if (frameCont1.Equals("Electrode") && frameCont2.Equals("Swablu"))
            {
                UserControl_Swablu.cancelar("");
                UserControl_Electrode.atacar2();
            }
            
        }

        private void metodoCPU(object sender, object e)
        {
            if (frameCont1.Equals("Swablu") && frameCont2.Equals("Electrode"))
            {
                UserControl_Swablu.cancelar("cancelar");
                UserControl_Electrode.atacar2();
                UserControl_Swablu.recibirDanio();
            }

            if (frameCont1.Equals("Swablu") && frameCont2.Equals("Charmander"))
            {
                UserControl_Swablu.cancelar("cancelar");
                //UserControl_Charmander.atacar2();
                UserControl_Swablu.recibirDanio();
            }

            if (frameCont1.Equals("Charmander") && frameCont2.Equals("Swablu"))
            {
                UserControl_Charmander.cancelar("cancelar");
                UserControl_Swablu.atacar2();
                UserControl_Charmander.recibirDanio();
            }

            if (frameCont1.Equals("Charmander") && frameCont2.Equals("Electrode"))
            {
                UserControl_Charmander.cancelar("cancelar");
                UserControl_Electrode.atacar2();
                UserControl_Charmander.recibirDanio();
            }

            if (frameCont1.Equals("Electrode") && frameCont2.Equals("Charmander"))
            {
                UserControl_Electrode.cancelar("cancelar");
                //UserControl_Charmander.atacar2();
                UserControl_Electrode.recibirDanio();
            }

            if (frameCont1.Equals("Electrode") && frameCont2.Equals("Swablu"))
            {
                UserControl_Electrode.cancelar("cancelar");
                UserControl_Swablu.atacar2();
                UserControl_Electrode.recibirDanio();
            }
        }

        public static void pararReloj()
        {
            if (reloj.IsEnabled)
            {
                reloj.Stop();
            }
        }
    }
}
