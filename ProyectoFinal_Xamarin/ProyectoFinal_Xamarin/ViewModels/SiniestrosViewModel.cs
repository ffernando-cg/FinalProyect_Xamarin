using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.Services;
using ProyectoFinal_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoFinal_Xamarin.ViewModels
{
    public class SiniestrosViewModel : BaseViewModel
    {
        static SiniestrosViewModel _instance;

        Command refreshCommand;
        public Command RefreshCommand => refreshCommand ?? (refreshCommand = new Command(RefreshSiniestros));


        Command _newCommand;
        public Command NewCommand => _newCommand ?? (_newCommand = new Command(NewAction));


        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));
        //public Command<TripModel> SelectExistingCommand => _selectExistingCommand ?? (_selectExistingCommand = new Command<TripModel>(SelectExistingAction));

        SiniestrosModel siniestroSelected;
        public SiniestrosModel SiniestroSelected
        {
            get => siniestroSelected;
            set => SetProperty(ref siniestroSelected, value);
        }

        ObservableCollection<SiniestrosModel> _Siniestros;
        public ObservableCollection<SiniestrosModel> Siniestros
        {
            get => _Siniestros;
            set => SetProperty(ref _Siniestros, value);
        }

        public SiniestrosViewModel()
        {
            
            // Indicamos a esta variable apunte a esta instancia
            _instance = this;
            Title = "Lista de Siniestros"; 

            LoadSiniestros();
        }

        private async void LoadSiniestros()
        {
            IsBusy = true;
            ApiResponse response = await new ApiService().GetDataAsync<SiniestrosModel>("siniestros");

            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error al cargar los siniestros", response.Message, "Ok");
                return;
            }
            Siniestros = (ObservableCollection<SiniestrosModel>)response.Result;
            IsBusy = false;
        }

        public static SiniestrosViewModel GetInstance()
        {
            if (_instance == null) _instance = new SiniestrosViewModel();
            return _instance;
        }

        private void NewAction()
        {
            MainPage Mp = (MainPage)Application.Current.MainPage;
            Mp.Detail.Navigation.PushAsync(new SiniestroDetailPage());
        }

        private void SelectAction()
        {
            MainPage Mp = (MainPage)Application.Current.MainPage;
            Mp.Detail.Navigation.PushAsync(new SiniestroDetailPage(SiniestroSelected));
        }

        public void RefreshSiniestros()
        {
            LoadSiniestros();
        }

    }
}
