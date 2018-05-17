using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNotes.Models;

namespace MyNotes.Models
{
    public class NoteUserViewModel
    {
        public List<Notes> Notes { get; set; }
        public string user { get; set; }
    }
}