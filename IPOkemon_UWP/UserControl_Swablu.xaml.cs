using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class UserControl_Swablu : UserControl
    {
        public static String cancelarPro = "";
        DispatcherTimer dt1;
        static Storyboard volar;
        static Storyboard atacado;
        static Storyboard ataque;
        Storyboard sinEnergia;
        Storyboard sinVida;

        public UserControl_Swablu()
        {
            this.InitializeComponent();

            this.Swablu.IsHitTestVisible = true;

            volar = (Storyboard)this.Resources["volar"];
            atacado = (Storyboard)this.Resources["atacado"];
            ataque = (Storyboard)this.Resources["ataque"];
            this.sinEnergia = (Storyboard)this.Resources["sinEnergia"];
            this.sinVida = (Storyboard)this.Resources["sinVida"];

            volar.Begin();
            dt1 = new DispatcherTimer();
            dt1.Interval = TimeSpan.FromSeconds(2);
            dt1.Start();
        }

        public static void cancelar(String nombre)
        {
            cancelarPro = nombre;
        }

        //ATACADO
        private void Swablu_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if(cancelarPro.Length == 0)
            {
                atacado.Completed += finAtacado;

                volar.Stop();
                ataque.Stop();
                atacado.Begin();

                Random r = new Random();
                int num = r.Next(0, 16);
                CombateSwablu.pbVidaSwablu.Value -= num;
            }
        }

        private static void finAtacado(object sender, object e)
        {
            volar.Begin();
        }

        public static void atacar2()
        {
            ataque.Completed += finAtaque;

            ataque.Stop();
            volar.Stop();
            atacado.Stop();
            ataque.Begin();
        }

        public static void recibirDanio()
        {
            atacado.Completed += finAtacado;

            volar.Stop();
            ataque.Stop();
            atacado.Begin();

            Random r = new Random();
            int num = r.Next(0, 16);
            CombateSwablu.pbVidaSwablu.Value -= num;
        }

        //ATACAR
        private void imgAtaque_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ataque.Completed += finAtaque;

            ataque.Stop();
            volar.Stop();
            atacado.Stop();
            ataque.Begin();
        }

        private static void finAtaque(object sender, object e)
        {
            volar.Begin();
        }

        //QUEDA POCA VIDA
        private void energiaBaja()
        {
            volar.Stop();
            atacado.Stop();
            ataque.Stop();

            this.sinEnergia.Begin();
        }

        private void finPocaEnergia(object sender, object e)
        {
            volar.Begin();
        }

        // SIN VIDA
        private async Task finAsync()
        {
            dt1.Stop();

            volar.Stop();
            atacado.Stop();
            ataque.Stop();
            this.sinEnergia.Stop();

            this.sinVida.Begin();

            this.Swablu.IsHitTestVisible = false;

            MessageDialog dialog = new MessageDialog("Me he quedado sin vida :(", "FIN");
            await dialog.ShowAsync();
        }
    }
}
