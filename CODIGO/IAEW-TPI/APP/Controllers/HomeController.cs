using APP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace APP.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NuevaReserva(int? id)
        {
            //Se deberia hacer tiro a la api para obtener datos del modelo de un vehiculo
            DetalleReserva dr = new DetalleReserva();
            dr.IDVehiculoCiudad = (int)id;
            ViewBag.Title = "Nueva Reserva";
            return View(dr);
        }

        public ActionResult Vehiculos()
        {
            string idCiudad = Request.Form["idCiudad"];
            if (idCiudad != null)
            {
                ViewBag.idCiudad = idCiudad;
                GetListadoVehiculos(int.Parse(idCiudad));
            }
            else
            {
                GetListadoVehiculos(1);
            }

            return View();
        }

        public ActionResult GetListadoVehiculos(int idCiudad)
        {
            try
            {
                //Inicializamos el objeto WebRequest
                var req = WebRequest.Create(@"http://localhost:26812/api/Vehiculos/ListadoVehiculos?idCiudad=" + idCiudad);

                //Indicamos el método a utilizar
                req.Method = "GET";
                //Definimos que el contenido del cuerpo del request tiene el formato Json
                req.ContentType = "application/json";

                //Realizamos la llamada a la API de la siguiente forma.
                var resp = req.GetResponse() as HttpWebResponse;
                if (resp != null && resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {
                        if (respStream != null)
                        {
                            //Obtenemos de la siguiente el cuerpo de la respuesta
                            var reader = new StreamReader(respStream, Encoding.UTF8);
                            string result = reader.ReadToEnd();

                            //El cuerpo en formato Json lo deserealizamos en el objeto usuario
                            List<Vehiculos> listResult = JsonConvert.DeserializeObject<List<Vehiculos>>(result);

                            ViewBag.Vehiculos = listResult;
                            return View();
                        }
                        return View();
                    }
                    //return View();
                }
                else
                {
                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        public ActionResult Reservas()
        {
            this.GetListaReservas();
            this.CancelarReserva();
            return View();
        }

        public ActionResult GetListaReservas()
        {
            try
            {
                var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/ListadoReservas");

                req.Method = "GET";
                req.ContentType = "application/json";

                var resp = req.GetResponse() as HttpWebResponse;
                if (resp != null && resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {
                        if (respStream != null)
                        {
                            var reader = new StreamReader(respStream, Encoding.UTF8);
                            string result = reader.ReadToEnd();

                            //El cuerpo en formato Json lo deserealizamos en el objeto usuario
                            var listResult = JsonConvert.DeserializeObject<List<Reserva>>(result);

                            ViewBag.Reservas = listResult;
                            return View();
                        }
                        return View();
                    }
                    //return View();
                }
                else
                {
                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }



        public ActionResult DetalleReserva()
        {

            string codigoReserva = Request.Form["codigoReserva"];
            if (codigoReserva != null)
            {
                this.GetDetalleReserva(codigoReserva);
            }
            else
            {
                this.GetDetalleReserva("");
            }
            return View();
        }
        public ActionResult GetDetalleReserva(string codigo)
        {
            //No se si esta bien esa URL
            try
            {
                var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/ConsultarReserva?codigo=" + codigo);

                req.Method = "GET";
                req.ContentType = "application/json";

                var resp = req.GetResponse() as HttpWebResponse;
                if (resp != null && resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {
                        if (respStream != null)
                        {
                            var reader = new StreamReader(respStream, Encoding.UTF8);
                            string result = reader.ReadToEnd();

                            var reserva = JsonConvert.DeserializeObject<Reserva>(result);

                            ViewBag.DetalleReserva = reserva;
                            return View();
                        }
                        return View();
                    }
                }
                else
                {
                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        public ActionResult CancelarReserva()
        {

            string codigoReserva = Request.Form["cancelarReserva"];
            if (codigoReserva != null)
            {
                try
                {

                    var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/CancelarReserva?codigo=" + codigoReserva);

                    req.Method = "DELETE";
                    req.ContentType = "application/json";

                    Stream dataStream = req.GetRequestStream();
                    dataStream.Write(Encoding.UTF8.GetBytes(codigoReserva), 0, codigoReserva.Length);
                    dataStream.Close();

                    return View();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }

                
            return View();
        }


        public ActionResult CrearReserva(DetalleReserva detalleReserva)
        {
            try
            {
                var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/ReservarVehiculo");

                req.Method = "POST";
                req.ContentType = "application/json";

                Stream dataStream = req.GetRequestStream();
                dataStream.Write(this.ToByteArray(detalleReserva), 0, 0);
                dataStream.Close();

                return View();

                /*
                var resp = req.GetResponse() as HttpWebResponse;
                if (resp != null && resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {
                        if (respStream != null)
                        {
                            var writer = new StreamWriter(respStream);
                            var reader = new StreamReader(respStream, Encoding.UTF8);
                            string result = reader.ReadToEnd();

                            var listResult = JsonConvert.DeserializeObject<List<Reserva>>(result);

                            ViewBag.Reservas = listResult;
                            return View();
                        }
                        return View();
                    }
                    //return View();
                }
                else
                {
                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
                    return View();
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        //public ActionResult NuevaReserva()
        //{
            
        //    DetalleReserva reserva = new DetalleReserva();

        //    string apenom = Request.Form["ApellidoNombre"];
        //    if (apenom != null)
        //    {
        //        reserva.ApellidoNombreCliente = apenom;
        //    }

        //    string doc = Request.Form["NroDocumento"];
        //    if (doc != null)
        //    {
        //        reserva.NroDocumentoCliente = doc;
        //    }

        //    string fechaRetiro = Request.Form["FechaRetiro"];
        //    if (fechaRetiro != null)
        //    {
        //        reserva.FechaHoraRetiro = DateTime.Parse(fechaRetiro);
        //    }

        //    string fechaDevolucion = Request.Form["FechaDevolucion"];
        //    if (fechaDevolucion != null)
        //    {
        //        reserva.FechaHoraDevolucion = DateTime.Parse(fechaDevolucion);
        //    }

        //    string id = Request.Form["IDVehiculo"];
        //    if (id != null)
        //    {
        //        reserva.IDVehiculoCiudad = int.Parse(id);
        //    }

        //    string lugarRetiro = Request.Form["LugarRetiro"];
        //    if (lugarRetiro != null)
        //    {
        //        reserva.LugarRetiro = lugarRetiro;
        //    }

        //    string lugarDevolucion = Request.Form["LugarDevolucion"];
        //    if (lugarDevolucion != null)
        //    {
        //        reserva.LugarDevolucion = lugarDevolucion;
        //    }

        //    this.CrearReserva(reserva);
        //    return View();
        //}


        public byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}