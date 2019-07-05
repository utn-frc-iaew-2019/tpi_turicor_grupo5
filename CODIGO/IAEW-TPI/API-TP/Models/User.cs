using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Documento { get; set; }
        public string Usuario { get; set; }

    }
}