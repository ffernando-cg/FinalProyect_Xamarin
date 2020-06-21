using ProyectoFinal_Xamarin.Models;
using ProyectoFinal_Xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinal_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SiniestroDetailPage : ContentPage
    {
        public SiniestroDetailPage()
        {
            InitializeComponent();
            BindingContext = new SiniestroDetailViewModel();
        }

        public SiniestroDetailPage(SiniestrosModel siniestro)
        {
            InitializeComponent();
            
            BindingContext = new SiniestroDetailViewModel(siniestro);
            lstViewAsegurados.SelectedItem = siniestro.Asegurado;
        }
    }
}