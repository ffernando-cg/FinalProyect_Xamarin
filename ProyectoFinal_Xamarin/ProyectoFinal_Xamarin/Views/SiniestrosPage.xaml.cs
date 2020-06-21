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
    public partial class SiniestrosPage : ContentPage
    {
        public SiniestrosPage()
        {
            InitializeComponent();

            BindingContext = new SiniestrosViewModel(); 
        }
    }
}