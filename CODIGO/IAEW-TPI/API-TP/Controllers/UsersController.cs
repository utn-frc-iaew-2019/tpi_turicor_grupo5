using API_TP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace API_TP.Controllers
{
    //[Route("api/[controller]")] Esto se explicita en App_Start / WebApiConfig.cs
    public class UsersController : ApiController
    {
        private Entities1 db = new Entities1();

        //List<User> users = new List<User>
        //{
        //    new User { IdUser = 1, Name = "Juan Perez",
        //               UserName = "juan.perez", Password = "pass123",
        //               ModifiedDate = DateTime.Now},
        //    new User { IdUser = 2, Name = "Esteban Ruiz",
        //               UserName = "esteban.ruiz", Password = "pass123",
        //               ModifiedDate = DateTime.Now},
        //    new User { IdUser = 3, Name = "Ricardo Rodriguez",
        //               UserName = "ricardo.rodriguez", Password = "pass123",
        //               ModifiedDate = DateTime.Now},
        //};

        //[HttpGet]
        //public List<User> GetAllUsers()
        //{
        //    return users;
        //}

        //// GET: api/users/3
        //[HttpGet]
        //public IHttpActionResult GetUser(int id)
        //{
        //    var user = users.FirstOrDefault((p) => p.IdUser == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}



        [HttpPost]
        public IHttpActionResult PostUser(string name, string lastname, int doc, string email)
        {
            try
            {
                Cliente client = new Cliente();
                client.Nombre = name;
                client.Apellido = lastname;
                client.NroDocumento = doc;
                client.Usuario = email;

                db.Cliente.Add(client);
                db.SaveChanges();

                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}