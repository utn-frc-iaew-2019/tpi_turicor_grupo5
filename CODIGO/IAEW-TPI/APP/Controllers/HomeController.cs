using APP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vehiculos()
        {
            ViewBag.Message = "Your application description page.";

            var vehiculos = GetListadoVehiculos(1);

            return View();
        }

        public ActionResult GetListadoVehiculos(int idCiudad)
        {
            try
            {
                //Inicializamos el objeto WebRequest
                var req = WebRequest.Create(@"http://localhost:26812/api/Vehiculos/ListadoVehiculos?idCiudad="+idCiudad);

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
                            var listResult = JsonConvert.DeserializeObject<List<Vehiculos>>(result);

                            foreach (var vehiculo in listResult)
                            {
                                Console.WriteLine("---------------------------------------");
                                Console.WriteLine(" Marca:" + vehiculo.Marca);
                                Console.WriteLine(" Modelo:" + vehiculo.Modelo);
                                Console.WriteLine(" Cantidad Puertas:" + vehiculo.CantidadPuertas);
                                Console.WriteLine(" Puntaje:" + vehiculo.Puntaje);
                                Console.WriteLine(" Precio:" + vehiculo.Precio);
                            }
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}