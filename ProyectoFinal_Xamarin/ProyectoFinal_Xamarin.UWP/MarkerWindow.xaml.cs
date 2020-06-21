using ProyectoFinal_Xamarin.Models;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ProyectoFinal_Xamarin.UWP
{
    public sealed partial class MarkerWindow : UserControl
    {
        public MarkerWindow(SiniestrosModel Siniestros)
        {
            this.InitializeComponent();

            MarkerWindowImage.Source = new BitmapImage(new Uri(Siniestros.FotoSiniestro));
            MarkerWindowTitle.Text = Siniestros.NombreAjustador;
            MarkerWindowNotes.Text = Siniestros.Asegurado.Nombre;
        }
    }
}
