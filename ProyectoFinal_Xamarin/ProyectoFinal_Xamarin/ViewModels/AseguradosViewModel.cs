using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.Services;
using ProyectoFinal_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ProyectoFinal_Xamarin.ViewModels
{
    public class AseguradosViewModel : BaseViewModel
    {
        static AseguradosViewModel _instance;

        Command refreshCommand;
        public Command RefreshCommand => refreshCommand ?? (refreshCommand = new Command(RefreshAsegurados));

        Command _newCommand;
        public Command NewCommand => _newCommand ?? (_newCommand = new Command(NewAction));

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        AseguradosModel aseguradoSelected;
        public AseguradosModel AseguradoSelected
        {
            get => aseguradoSelected;
            set => SetProperty(ref aseguradoSelected, value);
        }

        ObservableCollection<AseguradosModel> _Asegurados;
        public ObservableCollection<AseguradosModel> Asegurados
        {
            get => _Asegurados;
            set => SetProperty(ref _Asegurados, value);
        }

        public AseguradosViewModel()
        {
            // Indicamos a esta variable apunte a esta instancia
            _instance = this;
            Title = "Lista de asegurados"; // ESTA LINEA ES PARA AGREGARLE UN TITULO A LA PAGINA DE ARRIBA PERO NO APARECE
            
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

        public static AseguradosViewModel GetInstance()
        {
            if (_instance == null) _instance = new AseguradosViewModel();
            return _instance;
        }

        private void NewAction()
        {
            MainPage Mp = (MainPage)Application.Current.MainPage;
            Mp.Detail.Navigation.PushAsync(new AseguradoDetailPage());
        }

        private void SelectAction()
        {
            MainPage Mp = (MainPage)Application.Current.MainPage;
            Mp.Detail.Navigation.PushAsync(new AseguradoDetailPage(AseguradoSelected));
        }

        public void RefreshAsegurados()
        {
            LoadAsegurados();
        }
    }
}
