using System;
using System.Collections.Generic;
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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace IPOkemon_UWP
{
    public sealed partial class UserControl_Charmander : UserControl
    {
        DispatcherTimer t1;
        public static String cancelarPro = "";
        public static Storyboard danio;
        public static Storyboard ataque;
        public Storyboard dormir;
        public static Storyboard quieto;

        public UserControl_Charmander()
        {
            this.InitializeComponent();
            danio = (Storyboard)this.Resources["apagado"];
            quieto = (Storyboard)this.Resources["quieto"];
            ataque = (Storyboard)this.Resources["ataque"];
            this.dormir = (Storyboard)this.Resources["dormir"];

            //this.danio.Completed += finDanio;
            //ataque.Completed += finFuego;

            quieto.Begin();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000);
            t1.Start();
        }

        public static void cancelar(String nombre)
        {
            cancelarPro = nombre;
        }

        public void charmander1_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (cancelarPro.Length == 0)
            {
                quieto.Stop();
                ataque.Stop();
                danio.Begin();
                quieto.Begin();

                Random r = new Random();
                int num = r.Next(0, 16);

                CombateCharmander.pbVidaCharmander.Value -= num;
            }
        }

        public static void recibirDanio()
        {
            quieto.Stop();
            ataque.Stop();
            danio.Begin();
            quieto.Begin();

            Random r = new Random();
            int num = r.Next(0, 16);
            CombateCharmander.pbVidaCharmander.Value -= num;
        }

        private void finDanio(object sender, object e)
        {
            quieto.Begin();
        }

        private static void finFuego(object sender, object e)
        {
            quieto.Begin();
        }

        private void fuego_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ataque.Stop();
            quieto.Stop();
            danio.Stop();
        }
    }
}
