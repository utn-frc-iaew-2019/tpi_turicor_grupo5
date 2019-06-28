using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TP.Models
{
    public class DetalleReserva
    {
        public string ApellidoNombreCliente { get; set; }
        public DateTime FechaHoraDevolucion { get; set; }
        public DateTime FechaHoraRetiro { get; set; }
        public int IDVehiculoCiudad { get; set; }
        public string LugarRetiro { get; set; }
        public string LugarDevolucion { get; set; }
        public string NroDocumentoCliente { get; set; }
    }
}