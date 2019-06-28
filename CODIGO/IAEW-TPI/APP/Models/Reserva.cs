using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Models
{
    public class Reserva
    {
        public string CodigoReserva { get; set; }
        public Nullable<System.DateTime> FechaReserva { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdVendedor { get; set; }
        public Nullable<double> Costo { get; set; }
        public Nullable<double> PrecioVenta { get; set; }
        public Nullable<int> IdVehiculoCiudad { get; set; }
        public Nullable<int> IdCiudad { get; set; }
        public Nullable<int> idPais { get; set; }
        public int Id { get; set; }

    }
}