using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MyNotes.Models;
using Newtonsoft.Json;

namespace MyNotes.Controllers.API
{
    public class NotesController : ApiController
    {
        private ApplicationDbContext database; //database object which gives access to database

        public NotesController()
        {
            database = new ApplicationDbContext(); //initialize object in constructor
        }

        // POST: api/notes/new
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateNote(Notes note) // POST request for creating new notes
        {
            if (User.Identity.Name != note.Email && !User.IsInRole("Admin"))
            {
                return StatusCode(HttpStatusCode.Forbidden); //if other user tries to send request he will be denied
            }

            if (!ModelState.IsValid)    //validator response
            {
                return BadRequest();
            }

            database.Notes.Add(note);
            database.SaveChanges();

            var allNotesInDb = database.Notes.ToList();

            var noteInDbId = allNotesInDb[allNotesInDb.Count - 1].Id;

            //return Ok();
            return Ok(noteInDbId);
        }

        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateNote(Notes note) //PUT request for updating notes
        {
            //Notes note = new JavaScriptSerializer().Deserialize<Notes>(json);

            if (User.Identity.Name != note.Email && !User.IsInRole("Admin"))
            {
                return StatusCode(HttpStatusCode.Forbidden); //if other user tries to send request he will be denied
            }

            if (!ModelState.IsValid)    //validator response
            {
                return BadRequest();
            }

            var noteInDb = database.Notes.SingleOrDefault(n => n.Id == note.Id);

            if(noteInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            noteInDb.Name = note.Name;
            noteInDb.Content = note.Content;
            database.SaveChanges();
            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteNote(int id)
        {
            var noteInDb = database.Notes.SingleOrDefault(n => n.Id == id);

            if(noteInDb == null)
            {
                return BadRequest(); // note with such id is not in DB
            }

            if (User.Identity.Name != noteInDb.Email && !User.IsInRole("Admin"))
            {
                return StatusCode(HttpStatusCode.Forbidden); //if other user tries to send request he will be denied
            }

            database.Notes.Remove(noteInDb);
            database.SaveChanges();
            return Ok();
        }
    }
}
