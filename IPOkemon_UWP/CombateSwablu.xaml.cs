﻿using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPOkemon_UWP
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CombateSwablu : Page
    {
        DispatcherTimer t1;
        public static ProgressBar pbVidaSwablu;

        public CombateSwablu()
        {
            this.InitializeComponent();

            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000);
            t1.Tick += relojAsync;
            t1.Start();

            pbVidaSwablu = pbVida;
        }

        async void relojAsync(object sender, object e)
        {
            if (this.pbVida.Value == 0)
            {
                //PagCombate.muertoPokemon();
                t1.Stop();

                ContentDialog deleteFileDialog;
                deleteFileDialog = new ContentDialog
                {
                    Title = "GAME OVER",
                    Content = "Swablu se ha quedado sin vida.",
                    PrimaryButtonText = "OK",
                    DefaultButton = ContentDialogButton.Primary
                };

                if (Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride.Equals("en-US"))
                {
                    deleteFileDialog = new ContentDialog
                    {
                        Title = "GAME OVER",
                        Content = "Swablu is dead.",
                        PrimaryButtonText = "OK",
                        DefaultButton = ContentDialogButton.Primary
                    };
                }

                ContentDialogResult result = await deleteFileDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    MainPage.FramePrincipal.Navigate(typeof(PagPokedex));
                }

                if (Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride.Equals("en-US"))
                {
                    new ToastContentBuilder()
                    .AddArgument("conversationId", 9813)
                    .AddText("Swablu is lose")
                    .AddInlineImage(new Uri("ms-appx:///Assets/Swablu.png"))
                    .Show();
                }
                else
                {
                    new ToastContentBuilder()
                    .AddArgument("conversationId", 9813)
                    .AddText("Swablu ha perdido")
                    .AddInlineImage(new Uri("ms-appx:///Assets/Swablu.png"))
                    .Show();
                }
            }
        }
    }
}
