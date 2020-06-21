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
    public partial class AseguradoDetailPage : ContentPage
    {
        public AseguradoDetailPage()
        {
            InitializeComponent();

            BindingContext = new AseguradoDetailViewModel();
        }

        public AseguradoDetailPage(AseguradosModel asegurado)
        {
            InitializeComponent();

            BindingContext = new AseguradoDetailViewModel(asegurado);
        }
    }
}