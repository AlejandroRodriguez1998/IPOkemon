﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPOkemon_UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PagConfi : Page
    {
        public PagConfi()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var hbIdioma = sender as HyperlinkButton;
            var lang = "es-ES";

            switch (hbIdioma.Name)
            {
                case "spanish":
                    lang = "es-Es";
                    break;
                case "english":
                    lang = "en-US";
                    break;
            }

            ApplicationLanguages.PrimaryLanguageOverride = lang;
            
            (Window.Current.Content as Frame)?.Navigate(typeof(MainPage), null);
        }
    }
}
