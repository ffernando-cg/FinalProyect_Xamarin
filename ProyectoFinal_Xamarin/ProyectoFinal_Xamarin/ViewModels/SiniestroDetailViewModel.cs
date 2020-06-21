using Plugin.Media;
using ProyectoFinal_Xamarin.Converters;
using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.Services;
using ProyectoFinal_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoFinal_Xamarin.ViewModels
{
    public class SiniestroDetailViewModel : BaseViewModel
    {
        private int id = 0;
        private SiniestrosModel siniestroSelected;
        private bool isCoordsSelected = false;
        private bool isCoordBtnClicked = false;

        bool _MostrarFecha;
        public bool MostrarFecha
        {
            get => _MostrarFecha;
            set => SetProperty(ref _MostrarFecha, value);
        }

        bool _MostrarBtnUbi;
        public bool MostrarBtnUbi
        {
            get => _MostrarBtnUbi;
            set => SetProperty(ref _MostrarBtnUbi, value);
        }

        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));

        Command _mapCommand;
        public Command MapCommand => _mapCommand ?? (_mapCommand = new Command(OpenMapAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationCommandByButton));


        ObservableCollection<AseguradosModel> _Asegurados;
        public ObservableCollection<AseguradosModel> Asegurados
        {
            get => _Asegurados;
            set => SetProperty(ref _Asegurados, value);
        }

        DateTime _TiempoSiniestro;
        public DateTime TiempoSiniestro
        {
            get => _TiempoSiniestro;
            set => SetProperty(ref _TiempoSiniestro, value);
        }

        string _NombreAjustador;
        public string NombreAjustador
        {
            get => _NombreAjustador;
            set => SetProperty(ref _NombreAjustador, value);
        }

        string _FotoSiniestro;
        public string FotoSiniestro
        {
            get => _FotoSiniestro;
            set => SetProperty(ref _FotoSiniestro, value);
        }

        AseguradosModel _Asegurado;
        public AseguradosModel Asegurado
        {
            get => _Asegurado;
            set => SetProperty(ref _Asegurado, value);
        }

        string _Latitude;
        public string Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        string _Longitude;
        public string Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        

        public SiniestroDetailViewModel()
        {
            MostrarFecha = false;
            MostrarBtnUbi = true;
            LoadAsegurados();
        }

        public SiniestroDetailViewModel(SiniestrosModel siniestro)
        {
            
            if (siniestro != null)
            {
                isCoordsSelected = true;
                MostrarFecha = true;
                MostrarBtnUbi = false;
                id = siniestro.IdSiniestro;
                TiempoSiniestro = siniestro.TiempoSiniestro;
                NombreAjustador = siniestro.NombreAjustador;
                FotoSiniestro = siniestro.FotoSiniestro;
                Asegurado = siniestro.Asegurado;
                Latitude = siniestro.Latitude;
                Longitude = siniestro.Longitude;
                siniestroSelected = siniestro;
            }
            else
            {
                MostrarFecha = false;
                MostrarBtnUbi = true;
            }
            LoadAsegurados();
        }

        private async void LoadAsegurados()
        {
            IsBusy = true;
            ApiResponse response = await new ApiService().GetDataAsync<AseguradosModel>("asegurados");
            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error al cargar los asegurados", response.Message, "Ok");
                return;
            }
            Asegurados = (ObservableCollection<AseguradosModel>)response.Result;
            IsBusy = false;
        }

        private async void SaveAction()
        {
            if (string.IsNullOrWhiteSpace(NombreAjustador))
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Asegurese de llenar todos los campos", "Ok");
                return;
            }
            if (Asegurado == null)
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Por favor seleccione un asegurado", "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(FotoSiniestro))
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Por favor tome o seleccione una foto", "Ok");
                return;
            }

            IsBusy = true;
            if (id == 0)
            {
                if(string.IsNullOrEmpty(Longitude) || string.IsNullOrEmpty(Latitude))
                {
                    await GetLocationAction();
                }
                ApiResponse response = await new ApiService().PostDataAsync("siniestros", new SiniestrosModel
                {
                    TiempoSiniestro = DateTime.Now,
                    NombreAjustador = this.NombreAjustador,
                    FotoSiniestro = this.FotoSiniestro,
                    Asegurado = this.Asegurado,
                    Latitude = this.Latitude,
                    Longitude = this.Longitude
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", "Error al crear el viaje", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
                    return;
                }
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("siniestros/" + id, new SiniestrosModel
                {
                    IdSiniestro = this.id,
                    TiempoSiniestro = this.TiempoSiniestro,
                    NombreAjustador = this.NombreAjustador,
                    FotoSiniestro = this.FotoSiniestro,
                    Asegurado = this.Asegurado,
                    Latitude = this.Latitude,
                    Longitude = this.Longitude
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Error al actualizar el siniestro", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppSiniestros", response.Message, "Ok");
                    return;
                }
            }

            IsBusy = false;
            isCoordsSelected = false;
            isCoordBtnClicked = false;
            SiniestrosViewModel.GetInstance().RefreshSiniestros();
            MainPage Mp = (MainPage)Application.Current.MainPage;
            await Mp.Detail.Navigation.PopAsync();
        }

        private async void DeleteAction()
        {
            IsBusy = true;

            ApiResponse response = await new ApiService().DeleteDataAsync("siniestros", id);
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Error al eliminar el siniestro", "Ok");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", response.Message, "Ok");
                return;
            }
            IsBusy = false;
            isCoordsSelected = false;
            isCoordBtnClicked = false;
            SiniestrosViewModel.GetInstance().RefreshSiniestros();
            MainPage Mp = (MainPage)Application.Current.MainPage;
            await Mp.Detail.Navigation.PopAsync();
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            FotoSiniestro = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Seleccionar fotografías no soportada", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            FotoSiniestro = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

        private async Task GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Latitude = location.Latitude.ToString();
                    Longitude = location.Longitude.ToString();
                    isCoordsSelected = true;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("Error", fnsEx.ToString(), "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("Error", fneEx.ToString(), "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("Error", pEx.ToString(), "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        private async void GetLocationCommandByButton()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Latitude = location.Latitude.ToString();
                    Longitude = location.Longitude.ToString();
                    isCoordsSelected = true;
                    isCoordBtnClicked = true;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("Error", fnsEx.ToString(), "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("Error", fneEx.ToString(), "OK");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("Error", pEx.ToString(), "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        private async void OpenMapAction()
        {
            if (isCoordsSelected)
            {
                if (isCoordBtnClicked)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Primero debe de guardar todos los datos antes de ver su ubicacion", "OK");
                }
                else
                { 
                    MainPage Mp = (MainPage)Application.Current.MainPage;
                    Mp.Detail.Navigation.PushAsync(new SiniestroMapPage(siniestroSelected));
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error","No tiene ninguna coordenada seleccionada", "OK");
            }
        }
    }
}
