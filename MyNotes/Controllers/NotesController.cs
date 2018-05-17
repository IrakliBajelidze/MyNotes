using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNotes.Models;

namespace MyNotes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private ApplicationDbContext database; //database object which gives access to database

        public NotesController()
        {
            database = new ApplicationDbContext(); //initialize object in constructor
        }

        [Route("notes")]
        public ActionResult Index(string user)
        {
            if (!User.IsInRole("Admin"))
            {
                if (user == null)
                {
                    var userInDb1 = new NoteUserViewModel
                    {
                        Notes = database.Notes.Where(u => u.Email == User.Identity.Name).ToList() //gets notes of current user
                    };

                    return View(userInDb1);
                }

                return View("Error", null, "You do not have access to this user's notes.");
            }

            if (user == null)
            {
                var userInDb2 = new NoteUserViewModel
                {
                    Notes = database.Notes.Where(u => u.Email == User.Identity.Name).ToList() //gets notes of current user
                };

                return View(userInDb2);
            }
            
            var userInDb = new NoteUserViewModel
            {
                Notes = database.Notes.Where(u => u.Email == user).ToList(), //gets notes of current user
                user = user
            };

            /*
            if (userInDb.Count == 0)
            {
                return View("Error", null, "User with email: " + user + " either does not exist or has no notes.");
            }
            */

            return View(userInDb);
        }

        [Route("notes/edit/{id?}")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)   //happens when ~/notes/edit
            {
                return View("Error", null, "Note ID was not provided.");
            }

            var noteInDb = database.Notes.SingleOrDefault(n => n.Id == id); //find note in DB with passed id

            if (noteInDb == null)    //note with such ID not in DB
            {
                return View("Error", null, "Note with such ID doesn't exist.");
            }

            if (noteInDb.Email != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return View("Error", null, "You do not have access to this note.");
            }

            return View(noteInDb);
        }

        [Route("notes/new")]
        public ActionResult New(string user)
        {
            if (user == null)
            {
                var Notes1 = new Notes
                {
                    Email = User.Identity.Name,
                    Id = 0
                };

                return View("Edit", Notes1);
            }

            if (!User.IsInRole("Admin") && user != null)
            {
                return View("Error", null, "You can't create notes for other");
            }

            var Notes = new Notes
            {
                Email = user,
                Id = 0
            };

            return View("Edit", Notes);
        }
    }
}