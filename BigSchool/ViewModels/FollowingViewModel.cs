using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class FollowingViewModel
    {
        [Required]
        public string LecturerId { get; set; }
        public IEnumerable<ApplicationUser> Lecturer { get; set; }
        public bool ShowAction { get; set; }
    }
}