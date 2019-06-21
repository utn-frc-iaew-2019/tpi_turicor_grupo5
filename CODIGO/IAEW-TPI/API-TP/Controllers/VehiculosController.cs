using API_TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_TP.Controllers
{
    public class VehiculosController : ApiController
    {
        private Entities1 db = new Entities1();

        [HttpGet]
        public IHttpActionResult ListadoVehiculos(int idCiudad)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ConsultarVehiculosRequest();
                request.IdCiudad = idCiudad;
                var valor = client.ConsultarVehiculosDisponibles(credentials, request);

                //SE LE SUMA UN 20% DEL VALOR
                foreach (var item in valor.VehiculosEncontrados)
                {
                    decimal precio = item.PrecioPorDia;
                    decimal precioFinal = precio + (precio/100*20);
                    item.PrecioPorDia = precioFinal;
                }

                return Ok(valor);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult ReservarVehiculo(string nomyape, DateTime fhdev, DateTime fhret, int idVehCiudad, string lugarDev, string lugarRet, int doc)
        {
            //TIRO EN POSTMAN
            //http://localhost:26812/api/Vehiculos/ReservarVehiculos?nomyape="Santiago Innocenti"&fhdev=2019-06-22T13:45:30&fhret=2019-06-23T13:45:30&idVehCiudad=58&lugarDev=Plaza Colon&lugarRet=Aeropuerto&doc=123456789


            //FALTA AGREGAR QUE RECIBA POR PARAMETRO EL ID DEL CLIENTE, Y GUARDARLO EN BD
            //TAMBIEN FALTA EL ID DE LA CIUDAD Y EL ID DEL PAIS, TAMBIEN GUARDAR EN BD  
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ReservarVehiculoRequest();
                request.ApellidoNombreCliente = nomyape;
                request.FechaHoraDevolucion = fhdev;
                request.FechaHoraRetiro = fhret;
                request.IdVehiculoCiudad = idVehCiudad;
                request.NroDocumentoCliente = ""+doc;
                var valor = client.ReservarVehiculo(credentials, request);

                decimal costoReserva = valor.Reserva.TotalReserva * (-1); //VIENE EN NEGATIVO, POR ESO SE MULTIPLICA POR -1
                decimal precioFinalReserva = costoReserva + costoReserva / 100 * 20;
                string codigoReserva = valor.Reserva.CodigoReserva;

                Reserva nuevaReserva = new Reserva();
                nuevaReserva.CodigoReserva = codigoReserva;
                nuevaReserva.FechaReserva = fhret;
                //nuevaReserva.IdCliente = ;
                nuevaReserva.Costo = (double)costoReserva;
                nuevaReserva.PrecioVenta = (double)precioFinalReserva;
                nuevaReserva.IdVehiculoCiudad = idVehCiudad;

                db.Reserva.Add(nuevaReserva);
                db.SaveChanges();

                return Ok(nuevaReserva.ToString());
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
