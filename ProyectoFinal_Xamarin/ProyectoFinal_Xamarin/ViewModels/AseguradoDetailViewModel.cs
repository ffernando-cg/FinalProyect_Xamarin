using Plugin.Media;
using ProyectoFinal_Xamarin.Converters;
using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.Services;
using ProyectoFinal_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoFinal_Xamarin.ViewModels
{
    public class AseguradoDetailViewModel : BaseViewModel
    {
        private int id = 0;

        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        string _Nombre;
        public string Nombre
        {
            get => _Nombre;
            set => SetProperty(ref _Nombre, value);
        }

        string _Foto;
        public string Foto
        {
            get => _Foto;
            set => SetProperty(ref _Foto, value);
        }

        string _Domicilio;
        public string Domicilio
        {
            get => _Domicilio;
            set => SetProperty(ref _Domicilio, value);
        }

        public AseguradoDetailViewModel()
        {
        }

        public AseguradoDetailViewModel(AseguradosModel asegurado)
        {
            if (asegurado != null)
            {
                id = asegurado.IdAsegurado;
                Nombre = asegurado.Nombre;
                Domicilio = asegurado.Domicilio;
                Foto = asegurado.Foto;
            }
        }

        private async void SaveAction()
        {
            if(string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Domicilio))
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Asegurese de llenar todos los campos", "Ok");
                return;
            }
            if (string.IsNullOrWhiteSpace(Foto))
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Por favor tome o seleccione una foto", "Ok");
                return;
            }

            IsBusy = true;
            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("asegurados", new AseguradosModel
                {
                    Nombre = this.Nombre,
                    Domicilio = this.Domicilio,
                    Foto = this.Foto
                });
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("asegurados/" + id, new AseguradosModel
                {
                    IdAsegurado = id,
                    Nombre = this.Nombre,
                    Domicilio = this.Domicilio,
                    Foto = this.Foto
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Error al actualizar el asegurado", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppSiniestros", response.Message, "Ok");
                    return;
                }
            }

            IsBusy = false;
            AseguradosViewModel.GetInstance().RefreshAsegurados();
            MainPage Mp = (MainPage)Application.Current.MainPage;
            await Mp.Detail.Navigation.PopAsync();
        }

        private async void DeleteAction()
        {
            IsBusy = true;

            ApiResponse response = await new ApiService().DeleteDataAsync("asegurados", id);
            MainPage Mp = (MainPage)Application.Current.MainPage;
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", "Error al eliminar el asegurado", "Ok");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("AppSiniestros", response.Message, "Ok");
                await Mp.Detail.Navigation.PopAsync();
                return;
            }
            IsBusy = false;
            AseguradosViewModel.GetInstance().RefreshAsegurados();
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

            Foto = await new ImageService().ConvertImageFileToBase64(file.Path);
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

            Foto = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

       
    } 
 
    
}
