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
        //Equivale a Consultar el Detalle de una Reserva
        public IHttpActionResult ConsultarReserva([FromUri] string codigo)
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

        ///cliente/idcliente/reservas
               
        [HttpGet]
        public IHttpActionResult ListadoReservas(Cliente cliente)
        {
            try
            {
                List<Reserva> reservas = db.Reserva.Where(x => x.FechaReserva > DateTime.Now && cliente.Id== x.IdCliente).ToList();

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

                db.Reserva.Remove(db.Reserva.Find(valor.Reserva.Id));
                db.SaveChanges();

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



        [HttpPost]
        //public IHttpActionResult ReservarVehiculo(Cliente cliente, DateTime fhdev, DateTime fhret, int idVehCiudad, int idCiudad, int idPais)
        public IHttpActionResult ReservarVehiculo([FromBody] Reserva reserva)
        {
            //TIRO EN POSTMAN
            //http://localhost:26812/api/Vehiculos/ReservarVehiculos?nomyape="Santiago Innocenti"&fhdev=2019-06-22T13:45:30&fhret=2019-06-23T13:45:30&idVehCiudad=58&lugarDev=Plaza Colon&lugarRet=Aeropuerto&doc=123456789


            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ReservarVehiculoRequest();
                //request.ApellidoNombreCliente = cliente.Apellido + cliente.Nombre;
                request.ApellidoNombreCliente = null;// nomyape;
                //request.FechaHoraDevolucion =;// fhdev;
                //request.FechaHoraRetiro = fhret;
                //request.IdVehiculoCiudad = ;// idVehCiudad;
                //request.NroDocumentoCliente = cliente.NroDocumento.ToString();
                //request.NroDocumentoCliente = ""+doc;
                var valor = client.ReservarVehiculo(credentials, request);

                decimal costoReserva = valor.Reserva.TotalReserva * (-1); //VIENE EN NEGATIVO, POR ESO SE MULTIPLICA POR -1
                decimal precioFinalReserva = costoReserva * (decimal)1.2;
                string codigoReserva = valor.Reserva.CodigoReserva;

                Reserva nuevaReserva = new Reserva();
                nuevaReserva.CodigoReserva = codigoReserva;
                nuevaReserva.FechaReserva = null;//fhret;
                //nuevaReserva.IdCliente = cliente.Id;
                nuevaReserva.Costo = (double)costoReserva;
                nuevaReserva.PrecioVenta = (double)precioFinalReserva;
                nuevaReserva.IdVehiculoCiudad = null;//idVehCiudad;
                //nuevaReserva.IdCiudad = idCiudad;
                //nuevaReserva.idPais = idPais;

                db.Reserva.Add(nuevaReserva);
                db.SaveChanges();

                return Ok(nuevaReserva.ToString());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


    }
}
