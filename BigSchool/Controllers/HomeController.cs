using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using BigSchool.ViewModels;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var loginUser = User.Identity.GetUserId();
            ViewBag.LoginUser = loginUser;
            var upcommingCourses = _dbContext.Courses.OrderBy(p => p.DateTime)
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .ToList()
                .Where(c => c.DateTime > DateTime.Now && c.isCanceled != true);
            foreach(Course i in upcommingCourses)
            {
                Attendance find = _dbContext.Attendances.FirstOrDefault(p => p.CourseId == i.Id && p.AttendeeId == loginUser);
                Following findF = _dbContext.Followings.FirstOrDefault(p => p.FolloweeId == i.LecturerId && p.FollowerId == loginUser);
                if(findF == null)
                    i.Lecturer.isFollowing = true;
                if(find != null)
                    i.isShowGoing = true;
            }
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

    }
}