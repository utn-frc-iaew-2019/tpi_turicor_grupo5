using API_TP.Models;
using Newtonsoft.Json;
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

        [HttpGet]
        [Route("api/Reservas/Paises")]
        public IHttpActionResult ObtenerPaises()
        {
            //TIRO EN POSTMAN: http://localhost:26812/api/Reservas/Paises
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var valor = client.ConsultarPaises(credentials);

                List<object> Paises = new List<object>();

                foreach (var pais in valor.Paises)
                {
                    Paises.Add(new { Id = pais.Id, Nombre = pais.Nombre });
                }

                return Json(Paises);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpGet]
        public IHttpActionResult ObtenerCiudad(int id)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ConsultarCiudadesRequest();
                request.IdPais = (int)id;
                var valor = client.ConsultarCiudades(credentials, request);

                List<object> Ciudades = new List<object>();

                foreach (var ciudad in valor.Ciudades)
                {
                    Ciudades.Add(new { Id = ciudad.Id, Nombre = ciudad.Nombre });
                }

                return Json(Ciudades);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Reservas/ConsultarReserva")]
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

                return Ok(new { ApellidoNombreCliente = valor.Reserva.ApellidoNombreCliente, CodigoReserva = valor.Reserva.CodigoReserva,
                Estado = valor.Reserva.Estado, FechaCancelacion = valor.Reserva.FechaCancelacion,
                FechaHoraDevolucion = valor.Reserva.FechaHoraDevolucion, FechaHoraRetiro = valor.Reserva.FechaHoraRetiro,
                FechaReserva = valor.Reserva.FechaReserva, Id = valor.Reserva.Id, LugarDevolucion = valor.Reserva.LugarDevolucion,
                LugarRetiro = valor.Reserva.LugarRetiro, NroDocumentoCliente = valor.Reserva.NroDocumentoCliente,
                TotalReserva = valor.Reserva.TotalReserva});
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        ///cliente/idcliente/reservas
               
        [HttpGet]
        [Route("api/Reservas/Listado")]
        public IHttpActionResult ListadoReservas(int idCliente)
        {
            try
            {
                List<Reserva> reservas = db.Reserva.Where(x => x.IdCliente == idCliente).ToList();
                //DateTime.Now.CompareTo(x.FechaReserva.ToString()) != 1 &&-
                List<object> ReservasDeUsuario = new List<object>();

                foreach (var item in reservas)
                {
                    ReservasDeUsuario.Add(new { CodigoReserva = item.CodigoReserva, FechaReserva = item.FechaReserva,
                    IdCliente = item.IdCliente, IdVendedor = item.IdVendedor, Costo = item.Costo,
                    PrecioVenta = item.PrecioVenta, IdVehiculoCiudad = item.IdVehiculoCiudad, IdCiudad = item.IdCiudad,
                    idPais = item.idPais, Id = item.Id});
                }

                return Ok(ReservasDeUsuario);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Reservas/CancelarReserva")]
        [AcceptVerbs("DELETE", "POST")]
        public IHttpActionResult CancelarReserva([FromUri] string codigoReserva)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.CancelarReservaRequest();
                request.CodigoReserva = codigoReserva;
                var valor = client.CancelarReserva(credentials, request);
                Reserva reserva = db.Reserva.Where(x => x.CodigoReserva == codigoReserva).FirstOrDefault();
                db.Reserva.Remove(reserva);
                db.SaveChanges();

                return Ok(reserva.CodigoReserva);
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
        [Route("api/Reservas/ReservarVehiculo")]
        public IHttpActionResult ReservarVehiculo([FromBody] DetalleReserva reserva)
        {
            //TIRO EN POSTMAN
            //http://localhost:26812/api/Vehiculos/ReservarVehiculos?nomyape="Santiago Innocenti"&fhdev=2019-06-22T13:45:30&fhret=2019-06-23T13:45:30&idVehCiudad=58&lugarDev=Plaza Colon&lugarRet=Aeropuerto&doc=123456789


            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ReservarVehiculoRequest();
                request.ApellidoNombreCliente = reserva.ApellidoNombreCliente;
                request.FechaHoraDevolucion = reserva.FechaHoraDevolucion;
                request.FechaHoraRetiro = reserva.FechaHoraRetiro;
                request.IdVehiculoCiudad = reserva.IDVehiculoCiudad;
                request.NroDocumentoCliente = reserva.NroDocumentoCliente;
                //request.LugarRetiro = reserva.LugarRetiro;
                //request.LugarDevolucion = reserva.LugarDevolucion;
                var valor = client.ReservarVehiculo(credentials, request);

                decimal costoReserva = valor.Reserva.TotalReserva; //VIENE EN NEGATIVO, POR ESO SE MULTIPLICA POR -1
                decimal precioFinalReserva = costoReserva * (decimal)1.2;
                string codigoReserva = valor.Reserva.CodigoReserva;

                Reserva nuevaReserva = new Reserva();
                nuevaReserva.IdCliente = reserva.IdUsuario;
                nuevaReserva.CodigoReserva = codigoReserva;
                nuevaReserva.FechaReserva = reserva.FechaHoraRetiro;

                nuevaReserva.Costo = (double)costoReserva;
                nuevaReserva.PrecioVenta = (double)precioFinalReserva;
                nuevaReserva.IdVehiculoCiudad = reserva.IDVehiculoCiudad;
                nuevaReserva.IdCiudad = valor.Reserva.VehiculoPorCiudadEntity.CiudadId;
                //nuevaReserva.idPais = valor.Reserva.VehiculoPorCiudadEntity;

                db.Reserva.Add(nuevaReserva);
                db.SaveChanges();

                return Ok(nuevaReserva.ToString());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


    }
}
