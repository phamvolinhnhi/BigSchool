using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.CategoryId,
                Place = viewModel.Place,
                isCanceled = false
            };

            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Mine");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            foreach (Course i in courses)
            {
                Following findF = _dbContext.Followings.FirstOrDefault(p => p.FolloweeId == i.LecturerId && p.FollowerId == userId);
                if (findF != null)
                    i.Lecturer.isFollowing = true;
            }
            var viewModel = new CourseViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated,
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var follows = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();
            var viewModel = new FollowingViewModel
            {
                Lecturer = follows,
                ShowAction = User.Identity.IsAuthenticated,
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses.Include(l => l.Lecturer)
                .Include(l => l.Category).ToList()
                .Where(a => a.LecturerId == userId && a.DateTime > DateTime.Now && a.isCanceled != true);

            var viewModel = new CourseViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated,
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = course.DateTime.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                CategoryId = course.CategoryId,
                Place = course.Place,
            };
            return View("Edit", viewModel);

            //var loginUser = User.Identity.GetUserId();
            //var course = _dbContext.Courses.FirstOrDefault(c => c.LecturerId == loginUser && c.Id == Id);
            //if (course == null)
            //    return HttpNotFound("Course not found");
            //course.Lis
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Edit", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);
            course.LecturerId = User.Identity.GetUserId();
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.CategoryId;
            course.Place = viewModel.Place;
            _dbContext.SaveChanges();
            return RedirectToAction("Mine");
        }

        //[Authorize]
        //public ActionResult Delete(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);
        //    var viewModel = new CourseViewModel
        //    {
        //        Categories = _dbContext.Categories.ToList(),
        //        Date = course.DateTime.ToString("dd/M/yyyy"),
        //        Time = course.DateTime.ToString("HH:mm"),
        //        CategoryId = course.CategoryId,
        //        Place = course.Place,
        //    };
        //    return View("Delete", viewModel);
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult Delete(int id, CourseViewModel courseViewModel)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var find = _dbContext.Courses.FirstOrDefault(c => c.Id == id && c.LecturerId == userId);
        //    find.isCanceled = true;
        //    _dbContext.SaveChanges();
        //    return RedirectToAction("Mine");
        //}
    }
}