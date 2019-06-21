using API_TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace API_TP.Controllers
{
    public class ReservasController : ApiController
    {
        private Entities1 db = new Entities1();

        //[HttpGet]
        //public IHttpActionResult ObtenerCiudad(int id)
        //{
        //    try
        //    {
        //        var client = new ServiceReference1.WCFReservaVehiculosClient();
        //        var credentials = Credenciales();

        //        var request = new ServiceReference1.ConsultarCiudadesRequest();
        //        request.IdPais = id;
        //        var valor = client.ConsultarCiudades(credentials, request);

        //        return Ok(valor);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpGet]
        public IHttpActionResult ConsultarReserva(string codigo)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ConsultarReservasRequest();
                request.CodigoReserva = codigo;
                var valor = client.ConsultarReserva(credentials, request);

                return Ok(valor);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
               
        [HttpGet]
        public IHttpActionResult ListadoReservas()
        {
            //HAY QUE AGREGAR POR PARAMETRO UN ID DE USUARIO, Y FILTRAR RESPUESTA SQL POR IDUSUARIO
            try
            {
                List<Reserva> reservas = db.Reserva.Where(x => x.FechaReserva > DateTime.Now).ToList();

                var json = new JavaScriptSerializer().Serialize(reservas);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult CancelarReserva(string codigo)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.CancelarReservaRequest();
                request.CodigoReserva = codigo;
                var valor = client.CancelarReserva(credentials, request);

                //HAY QUE ANALIZAR SI EN LA BD SE ELIMINA EL REGISTRO DE LA RESERVA, O SE LE CREA UNA COLUMNA DE ESTADORESERVA PARA TENER REGISTRO

                return Ok(valor);
            }
            catch(Exception ex)
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
