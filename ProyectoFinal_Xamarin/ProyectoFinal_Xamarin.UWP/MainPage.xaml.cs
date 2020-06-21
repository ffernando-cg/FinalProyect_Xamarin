using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ProyectoFinal_Xamarin.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.FormsMaps.Init("nRVTu0VrKWNxZN0md0m7~5FBX2EgePfeEHwIWLB-V5A~AtJgdOWQfAl8OnhgorJ5Ml4DL-A737R9xqhsawPqlinF-7uN0TohWU_wJdKvQMNu");

            LoadApplication(new ProyectoFinal_Xamarin.App());
        }
    }
}
