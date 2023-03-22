using BigSchool.ViewModels;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Security.Principal;

namespace BigSchool.Models
{
    public class Course
    {
        public int Id { get; set; }
        public ApplicationUser Lecturer { get; set; }
        [Required]
        public string LecturerId { get; set; }
        [Required, StringLength(255)]
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public Category Category { get; set; }
        [Required]
        public byte CategoryId { get; set; }

        public bool isShowGoing = false;

        public bool isCanceled { get; set; }

    }
}