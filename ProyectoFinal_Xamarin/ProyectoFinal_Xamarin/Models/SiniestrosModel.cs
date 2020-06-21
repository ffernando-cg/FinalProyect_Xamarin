using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal_Xamarin.Models
{
    public class SiniestrosModel
    {
        public int IdSiniestro { get; set; }
        public DateTime TiempoSiniestro { get; set; }
        public string NombreAjustador { get; set; }
        public string FotoSiniestro { get; set; }
        public AseguradosModel Asegurado { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
