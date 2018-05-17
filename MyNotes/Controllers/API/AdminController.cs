using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MyNotes.Controllers.API
{
    public class AdminController : ApiController
    {
        private ApplicationDbContext database; //database object which gives access to database

        public AdminController()
        {
            database = new ApplicationDbContext(); //initialize object in constructor
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteNote(string user)
        {
            if (!User.IsInRole("Admin"))
            {
                return StatusCode(HttpStatusCode.Forbidden); //if other user tries to send request he will be denied
            }

            var userInDb = database.Users.SingleOrDefault(u => u.Email == user);

            if (userInDb == null)
            {
                return BadRequest();
            }

            database.Users.Remove(userInDb);
            database.SaveChanges();
            return Ok();
        }
    }
}
