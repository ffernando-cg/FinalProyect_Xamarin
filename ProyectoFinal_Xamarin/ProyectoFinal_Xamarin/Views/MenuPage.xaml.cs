using ProyectoFinal_Xamarin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinal_Xamarin.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Introduccion, Title="Introduccion" },
                new HomeMenuItem {Id = MenuItemType.Asegurados, Title="Asegurados" },
                new HomeMenuItem {Id = MenuItemType.Siniestros, Title="Siniestros" },
                new HomeMenuItem {Id = MenuItemType.Integrantes, Title="Integrantes" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                MainPage Mp = (MainPage)Application.Current.MainPage;
                await Mp.Detail.Navigation.PopToRootAsync();
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}