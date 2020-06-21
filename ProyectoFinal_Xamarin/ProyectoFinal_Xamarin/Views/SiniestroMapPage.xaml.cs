using ProyectoFinal_Xamarin.Converters;
using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoFinal_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SiniestroMapPage : ContentPage
    {
        public SiniestroMapPage(SiniestrosModel siniestroSelected)
        {
            InitializeComponent();
                MapSiniestro.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(Double.Parse(siniestroSelected.Latitude), Double.Parse(siniestroSelected.Longitude)),
                        Distance.FromMiles(.5)
                ));

                string imagePath = new ImageService().SaveImageFromBase64(siniestroSelected.FotoSiniestro, siniestroSelected.IdSiniestro);
                siniestroSelected.FotoSiniestro = imagePath;
                MapSiniestro.Siniestros = siniestroSelected;

                MapSiniestro.Pins.Add(
                    new Pin
                    {
                        Type = PinType.Place,
                        Label = siniestroSelected.Asegurado.Nombre,
                        Position = new Position(Double.Parse(siniestroSelected.Latitude), Double.Parse(siniestroSelected.Longitude))
                    }
                );

                NombreAjustador.Text = siniestroSelected.NombreAjustador;
                Fecha.Text = siniestroSelected.TiempoSiniestro.ToString();
                Asegurado.Text = siniestroSelected.Asegurado.Nombre;

                
            }
    }
}