using API_TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_TP.Controllers
{
    public class ReservasController : ApiController
    {
        private Entities1 db = new Entities1();

        [HttpGet]
        public IHttpActionResult ObtenerCiudad(int id)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ConsultarCiudadesRequest();
                request.IdPais = id;
                var valor = client.ConsultarCiudades(credentials, request);

                return Ok(valor);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        private ServiceReference1.Credentials Credenciales()
        {
            var credentials = new ServiceReference1.Credentials();
            credentials.UserName = "grupo_nro5";
            credentials.Password = "YETmy9oCBn";

            return credentials;
        }



    }
}
