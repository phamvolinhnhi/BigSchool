using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class FollowingController : ApiController
    {
        // GET: Following
        private readonly ApplicationDbContext context;
        public FollowingController()
        {
            context = new ApplicationDbContext();
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [System.Web.Http.HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
                if (context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
                    return BadRequest("Following already exists!");
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            context.Followings.Add(following);
            context.SaveChanges();
            return Ok();
        }
    }
}