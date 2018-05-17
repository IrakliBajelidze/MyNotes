using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNotes.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext database; //database object which gives access to database

        public AdminController()
        {
            database = new ApplicationDbContext(); //initialize object in constructor
        }

        // GET: Admin
        [Route("admin")]
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return View("Error", null, "You don't have access to view this.");
            }

            var list = database.Users.ToList();

            return View("AdminView", list);
        }
    }
}