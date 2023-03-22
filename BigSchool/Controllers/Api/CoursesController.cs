using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _db { get; set; }
        public CoursesController()
        {
            _db = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _db.Courses.Single(c=> c.Id == id && c.LecturerId == userId);
            if(course.isCanceled)
                return NotFound();
            course.isCanceled = true;
            _db.SaveChanges();
            return Ok();
        }
    }
}
