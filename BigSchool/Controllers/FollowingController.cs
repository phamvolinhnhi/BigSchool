using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private readonly ApplicationDbContext context;
        public FollowingController()
        {
            context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            //if (context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            //    return BadRequest("Following already exists!");
            //var following = new Following
            //{
            //    FollowerId = userId,
            //    FolloweeId = followingDto.FolloweeId
            //};
            //context.Followings.Add(following);

            Following find = context.Followings.FirstOrDefault(f => f.FollowerId ==  userId && f.FolloweeId == followingDto.FolloweeId);
            if (find == null)
            {
                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = followingDto.FolloweeId
                };
                context.Followings.Add(following);
            }
            else
                context.Followings.Remove(find);
            context.SaveChanges();
            return Ok();
        }

    }
}