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
            //var upcommingCourses = _dbContext.Courses
            //    .Include(c => c.Lecturer)
            //    .Include(c => c.Category)
            //    .Where(c => c.DateTime > DateTime.Now);
            //return View(upcommingCourses);
            var loginUser = User.Identity.GetUserId();
            ViewBag.LoginUser = loginUser;
            var upcommingCourses = _dbContext.Courses.OrderBy(p => p.DateTime)
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .ToList()
                .Where(c => c.DateTime > DateTime.Now && c.isCanceled != true);
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}