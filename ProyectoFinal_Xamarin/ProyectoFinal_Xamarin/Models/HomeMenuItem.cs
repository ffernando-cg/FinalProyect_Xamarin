using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_Xamarin.Models
{
    public enum MenuItemType
    {
        Introduccion,
        Asegurados,
        Siniestros,
        Integrantes
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
