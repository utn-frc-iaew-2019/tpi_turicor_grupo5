using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Models
{
    public class Vehiculos
    {
        public string Id { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public int Puertas { get; set; }

        public double Puntaje { get; set; }

        public double Precio { get; set; }
    }
}