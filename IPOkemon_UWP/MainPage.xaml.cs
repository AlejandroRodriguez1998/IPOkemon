using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace IPOkemon_UWP
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Frame FramePrincipal;

        public MainPage()
        {
            this.InitializeComponent();
            FramePrincipal = MyFrame;
            RPinicio.Background = new SolidColorBrush(Colors.LightBlue);
            MyFrame.Navigate(typeof(PagInicio));
        }

        private void TBacercaDe_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            tbAcercaDe.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TBacercaDe_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tbAcercaDe.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void TBpokedex_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            tbPokedex.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TBpokedex_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tbPokedex.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void TBinicio_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            tbInicio.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TBinicio_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tbInicio.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void tbConfiguracion_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            tbConfiguracion.Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void tbConfiguracion_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            tbConfiguracion.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void RPpokedex_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            BorrarFondos();
            RPpokedex.Background = new SolidColorBrush(Colors.LightBlue);
            PagCombate.pararReloj();
            MyFrame.Navigate(typeof(PagPokedex));
        }

        private void RPacercaDe_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            BorrarFondos();
            RPacercaDe.Background = new SolidColorBrush(Colors.LightBlue);
            PagCombate.pararReloj();
            MyFrame.Navigate(typeof(PagAcercaDe));
        }

        private void RPconfiguracion_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            BorrarFondos();
            RPconfiguracion.Background = new SolidColorBrush(Colors.LightBlue);
            PagCombate.pararReloj();
            MyFrame.Navigate(typeof(PagConfi));
        }

        public void BorrarFondos()
        {
            RPinicio.Background = null;
            RPpokedex.Background = null;
            RPacercaDe.Background = null;
            RPconfiguracion.Background = null;
        }

        private void RPinicio_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            BorrarFondos();
            RPinicio.Background = new SolidColorBrush(Colors.LightBlue);
            PagCombate.pararReloj();
            MyFrame.Navigate(typeof(PagInicio));
        }
    }
}
