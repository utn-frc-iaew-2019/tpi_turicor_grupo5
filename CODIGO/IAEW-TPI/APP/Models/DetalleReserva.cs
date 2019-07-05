using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace APP.Models
{
    public class DetalleReserva
    {
        public int IdUsuario { get; set; }
        public string ApellidoNombreCliente { get; set; }
        public DateTime FechaHoraDevolucion { get; set; }
        public DateTime FechaHoraRetiro { get; set; }
        public int IDVehiculoCiudad { get; set; }
        public string LugarRetiro { get; set; }
        public string LugarDevolucion { get; set; }
        public string NroDocumentoCliente { get; set; }

    }
 
}