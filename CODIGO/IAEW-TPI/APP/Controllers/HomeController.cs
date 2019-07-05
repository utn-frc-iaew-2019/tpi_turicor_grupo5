using APP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
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
            ViewBag.IdUser = System.Web.HttpContext.Current.Session["sessionIdUser"];
            ViewBag.Title = "Nueva Reserva";
            return View(dr);
        }

        public ActionResult Vehiculos()
        {
            try
            {
                //http://localhost:26812/api/Reservas/Paises

                //BUSCAMOS PAISES
                //Inicializamos el objeto WebRequest
                var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/Paises");

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
                            List<Pais> listResult = JsonConvert.DeserializeObject<List<Pais>>(result);

                            List<SelectListItem> ListaPaises = new List<SelectListItem>();

                            //SelectListItem it = new SelectListItem();
                            //it.Text = "Seleccione";
                            //it.Value = "0";
                            //ListaPaises.Add(it);


                            foreach (var pais in listResult.OrderBy(e => e.Nombre))
                            {
                                SelectListItem item = new SelectListItem();
                                item.Text = pais.Nombre;
                                item.Value = pais.Id;

                                ListaPaises.Add(item);
                            }

                            ViewBag.Paises = ListaPaises;

                            try
                            {
                                //BUSCAMOS CIUDADES DE PAIS SELECCIONADO
                                //Inicializamos el objeto WebRequest
                                var req1 = WebRequest.Create(@"http://localhost:26812/api/Reservas/ObtenerCiudad?id=" + ListaPaises.First().Value);

                                //Indicamos el método a utilizar
                                req1.Method = "GET";
                                //Definimos que el contenido del cuerpo del request tiene el formato Json
                                req1.ContentType = "application/json";

                                //Realizamos la llamada a la API de la siguiente forma.
                                var resp1 = req1.GetResponse() as HttpWebResponse;
                                if (resp1 != null && resp1.StatusCode == HttpStatusCode.OK)
                                {
                                    using (var respStream1 = resp1.GetResponseStream())
                                    {
                                        if (respStream1 != null)
                                        {
                                            //Obtenemos de la siguiente el cuerpo de la respuesta
                                            var reader1 = new StreamReader(respStream1, Encoding.UTF8);
                                            string result1 = reader1.ReadToEnd();

                                            //El cuerpo en formato Json lo deserealizamos en el objeto usuario
                                            List<Ciudad> listResult1 = JsonConvert.DeserializeObject<List<Ciudad>>(result1);

                                            List<SelectListItem> ListaCiudades = new List<SelectListItem>();

                                            //SelectListItem it1 = new SelectListItem();
                                            //it1.Text = "Seleccione";
                                            //it1.Value = "0";
                                            //ListaCiudades.Add(it1);

                                            foreach (var ciudad in listResult1.OrderBy(e => e.Nombre))
                                            {
                                                SelectListItem item = new SelectListItem();
                                                item.Text = ciudad.Nombre;
                                                item.Value = ciudad.Id;

                                                ListaCiudades.Add(item);
                                            }

                                            ViewBag.Ciudades = ListaCiudades;
                                            return View();
                                        }
                                        return View();
                                    }
                                    //return View();
                                }
                                else
                                {
                                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp1.StatusCode, resp1.StatusDescription);
                                    return View();
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                return null;
                            }
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

        public JsonResult ObtenerCiudadesPorPais(int idPais)
        {
            try
            {
                //BUSCAMOS CIUDADES DE PAIS SELECCIONADO
                //Inicializamos el objeto WebRequest
                var req1 = WebRequest.Create(@"http://localhost:26812/api/Reservas/ObtenerCiudad?id=" + idPais);

                //Indicamos el método a utilizar
                req1.Method = "GET";
                //Definimos que el contenido del cuerpo del request tiene el formato Json
                req1.ContentType = "application/json";

                //Realizamos la llamada a la API de la siguiente forma.
                var resp1 = req1.GetResponse() as HttpWebResponse;
                if (resp1 != null && resp1.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream1 = resp1.GetResponseStream())
                    {
                        if (respStream1 != null)
                        {
                            //Obtenemos de la siguiente el cuerpo de la respuesta
                            var reader1 = new StreamReader(respStream1, Encoding.UTF8);
                            string result1 = reader1.ReadToEnd();

                            //El cuerpo en formato Json lo deserealizamos en el objeto usuario
                            List<Ciudad> listResult1 = JsonConvert.DeserializeObject<List<Ciudad>>(result1);
                            
                            return Json(new { listado = listResult1 }, JsonRequestBehavior.AllowGet);
                        }
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp1.StatusCode, resp1.StatusDescription);
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

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

                            return Json(new { listado = listResult }, JsonRequestBehavior.AllowGet);

                            //ViewBag.Vehiculos = listResult;
                            //return View();
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

        public ActionResult Reservar(int idUsuario, string apeynom, string doc, DateTime fechaRetiro, DateTime fechaDevolucion, int idVehiculo, string lugarRetiro, string lugarDevolucion)
        {
            try
            {
                DetalleReserva dr = new DetalleReserva();
                dr.IdUsuario = idUsuario;
                dr.ApellidoNombreCliente = apeynom;
                dr.NroDocumentoCliente = doc;
                dr.FechaHoraRetiro = fechaRetiro;
                dr.FechaHoraDevolucion = fechaDevolucion;
                dr.IDVehiculoCiudad = idVehiculo;
                dr.LugarRetiro = lugarRetiro;
                dr.LugarDevolucion = lugarDevolucion;

                //Inicializamos el objeto WebRequest
                var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/ReservarVehiculo");

                //Indicamos el método a utilizar
                req.Method = "POST";
                //Definimos que el contenido del cuerpo del request tiene el formato Json
                req.ContentType = "application/json";

                //Escribimos sobre el cuerpo del request los datos del usuario en formato Json
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    //Serializamos el objeto usuario en un string con formato Json
                    var jsonData = JsonConvert.SerializeObject(dr);

                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                //Realizamos la llamada a la API de la siguiente forma.
                var resp = req.GetResponse() as HttpWebResponse;

                //El protocolo HTTP define un cambpo Status que indica el estado de la peticion.
                //StatusCode = 200 (OK), indica que la llamada se proceso correctamente.
                //Cualquier otro caso corresponde a un error.
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
                            Reserva res = JsonConvert.DeserializeObject<Reserva>(result);

                            return Json(new { reserva = res }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    return null;
                }
                else
                {
                    Console.WriteLine("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription);
                    return null;
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
                int idUsuario = (int)System.Web.HttpContext.Current.Session["sessionIdUser"];
                var req = WebRequest.Create(@"http://localhost:26812/api/Reservas/Listado?idCliente="+idUsuario);

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



        public ActionResult DetalleReserva(string codigo)
        {

            //string codigoReserva = Request.Form["codigoReserva"];
            if (codigo != null)
            {
                this.GetDetalleReserva(codigo);
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

                            var reserva = JsonConvert.DeserializeObject<ConsultaReserva>(result);
                            reserva.FechaHoraRetiro = DateTime.ParseExact(reserva.FechaHoraRetiro.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            reserva.FechaHoraDevolucion = DateTime.ParseExact(reserva.FechaHoraDevolucion.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
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