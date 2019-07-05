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
        public IHttpActionResult PostUser([FromBody] User usuario)
        {
            try
            {
                Cliente client = new Cliente();
                client.Nombre = usuario.Nombre;
                client.Apellido = usuario.Apellido;
                client.NroDocumento = usuario.Documento;
                client.Usuario = usuario.Usuario;

                db.Cliente.Add(client);
                db.SaveChanges();
                db.Entry(client).GetDatabaseValues();


                return Ok(new { Id = client.Id, Nombre = client.Nombre, Apellido = client.Apellido,
                Documento = client.NroDocumento, Usuario = client.Usuario});
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult UserExist(string mail)
        {
            try
            {
                Cliente client = db.Cliente.Where(x => x.Usuario.Equals(mail)).FirstOrDefault();
                if(client != null)
                {
                    return Ok(new { Id = client.Id, Nombre = client.Nombre, Apellido = client.Apellido,
                    Documento = client.NroDocumento, Usuario = client.Usuario});
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}