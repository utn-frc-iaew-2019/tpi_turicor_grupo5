using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Models
{
    public class ConsultaReserva
    {
        public string ApellidoNombreCliente { get; set; }
        public string CodigoReserva { get; set; }
        public int Estado { get; set; }
        public Nullable<DateTime> FechaCancelacion { get; set; }
        public Nullable<DateTime> FechaHoraDevolucion { get; set; }
        public Nullable<DateTime> FechaHoraRetiro { get; set; }
        public Nullable<DateTime> FechaReserva { get; set; }
        public int Id { get; set; }
        public string LugarDevolucion { get; set; }
        public string LugarRetiro { get; set; }
        public string NroDocumentoCliente { get; set; }
        public double TotalReserva { get; set; }


    }
}