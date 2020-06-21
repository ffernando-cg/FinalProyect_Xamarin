using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.UWP.Renderers;
using ProyectoFinal_Xamarin.Renderers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;


[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace ProyectoFinal_Xamarin.UWP.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        MapControl nativeMap;
        MarkerWindow markerWindow;
        bool markerWindowShown = false;
        SiniestrosModel Siniestros;
        string Latitude, Longitude;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                nativeMap.MapElementClick -= OnMapElementClick;
                nativeMap.Children.Clear();
                markerWindow = null;
                nativeMap = null;
            }

            if (e.NewElement != null)
            {
                    this.Siniestros = (e.NewElement as CustomMap).Siniestros;

                    var formsMap = (CustomMap)e.NewElement;
                    nativeMap = Control as MapControl;
                    nativeMap.Children.Clear(); // ay lmao
                    nativeMap.MapElementClick += OnMapElementClick;

                    var snPosition = new BasicGeoposition
                    {
                        Latitude = Double.Parse(Siniestros.Latitude),
                        Longitude = Double.Parse(Siniestros.Longitude)
                    };
                    var snPoint = new Geopoint(snPosition);

                    var mapIcon = new MapIcon();
                    mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///pin.png"));
                    mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    mapIcon.Location = snPoint;
                    mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                    nativeMap.MapElements.Add(mapIcon);
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if (mapIcon != null)
            {
                if (!markerWindowShown)
                {
                    if (markerWindow == null) markerWindow = new MarkerWindow(Siniestros);

                    var snPosition = new BasicGeoposition
                    {
                        Latitude = Double.Parse(Siniestros.Latitude),
                        Longitude = Double.Parse(Siniestros.Longitude)
                    };
                    var snPoint = new Geopoint(snPosition);

                    nativeMap.Children.Add(markerWindow);
                    MapControl.SetLocation(markerWindow, snPoint);
                    MapControl.SetNormalizedAnchorPoint(markerWindow, new Windows.Foundation.Point(0.5, 1.0));

                    markerWindowShown = true;
                }
                else
                {
                    nativeMap.Children.Remove(markerWindow);
                    markerWindowShown = false;
                }
            }
        }
    }
}
