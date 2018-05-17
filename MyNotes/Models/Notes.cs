using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNotes.Models
{
    public class Notes
    {
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Too long note name.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(1024, ErrorMessage = "Content can't exceed 1024 characters.")]
        public string Content { get; set; }
    }
}