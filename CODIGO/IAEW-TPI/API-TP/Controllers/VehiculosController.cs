using API_TP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml;

namespace API_TP.Controllers
{
    public class VehiculosController : ApiController
    {
        private Entities1 db = new Entities1();

        [HttpGet]
        public IHttpActionResult ListadoVehiculos([FromUri] int idCiudad)
        {
            try
            {
                var client = new ServiceReference1.WCFReservaVehiculosClient();
                var credentials = Credenciales();

                var request = new ServiceReference1.ConsultarVehiculosRequest();
                request.IdCiudad = idCiudad;
                var valor = client.ConsultarVehiculosDisponibles(credentials, request);

                List<object> vehiculos = new List<object>();
                //SE LE SUMA UN 20% DEL VALOR
                foreach (var item in valor.VehiculosEncontrados)
                {
                    decimal precio = item.PrecioPorDia;
                    decimal precioFinal = precio * (decimal)1.2;
                    item.PrecioPorDia = precioFinal;
                    

                    //No se por que no reconoce o agrega el ID del Vehiculo
                    vehiculos.Add(new {Id = ""+item.Id, Marca = ""+item.Marca, Modelo = ""+item.Modelo, Puertas = ""+item.CantidadPuertas,
                    Puntaje = ""+item.Puntaje, Precio = ""+precioFinal});
                }
                return Json(vehiculos);
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
