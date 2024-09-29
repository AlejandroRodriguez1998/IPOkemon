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
    public sealed partial class UserControl_Electrode : UserControl
    {
        static Storyboard muerte;
        static Storyboard contento;
        static Storyboard rojoDolor;
        static Storyboard resucitar;
        static Storyboard flotarAccion;
        static Storyboard ataqueAnimacion;
        public static String cancelarPro = "";

        DispatcherTimer reloj = new DispatcherTimer();

        public UserControl_Electrode()
        {
            this.InitializeComponent();
            muerte = (Storyboard)this.Resources["morir"];
            resucitar = (Storyboard)this.Resources["revivir"];
            contento = (Storyboard)this.Resources["contento"];
            flotarAccion = (Storyboard)this.Resources["flotar"];
            ataqueAnimacion = (Storyboard)this.Resources["atacar"];
            rojoDolor = (Storyboard)this.mitadCuerpo.Resources["dolorKey"];

            flotarAccion.Begin();
        }

        public static void cancelar(String nombre)
        {
            cancelarPro = nombre;
        }

        public static void atacar2()
        {
            ataqueAnimacion.Begin();
        }

        public static void recibirDanio()
        {
            dolorAccion();

            Random r = new Random();
            int num = r.Next(0, 16);
            CombateElectrode.pbVidaElectrode.Value -= num;
        }

        private static void dolorAccion()
        {
            rojoDolor.Begin();
        }

        private void morir()
        {
            resucitar.Stop();
            muerte.Begin();
        }

        private void revivir()
        {
            muerte.Stop();
            resucitar.Begin();
        }

        private void atacar_MouseUp(object sender, object e)
        {
            if (this.reloj.IsEnabled)
            {
                this.reloj.Stop();
                contento.Stop();
            }
        }

        private void pokemon_MouseUp(object sender, PointerRoutedEventArgs e)
        {
            if (cancelarPro.Length == 0)
            {
                dolorAccion();

                Random r = new Random();
                int num = r.Next(0, 16);
                CombateElectrode.pbVidaElectrode.Value -= num;
            }
        }
    }
}
