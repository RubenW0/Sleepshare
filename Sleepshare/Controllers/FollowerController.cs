using BusinessLogicLayer.IRepositorys;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class FollowerController : Controller
    {

        private readonly FollowerService _followerService;

        public FollowerController(IConfiguration configuration)
        {
            _followerService = new FollowerService(new FollowerRepository(configuration));
        }

        [HttpGet]
        public IActionResult SearchUsers()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var users = _followerService.GetAllUsers(); 
            var followedUserIds = _followerService.GetFollowedUserIds(userId.Value); 

            ViewBag.FollowedUserIds = followedUserIds; 
            return View(users);
        }


        [HttpPost]
        public IActionResult Follow(int followsId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            _followerService.FollowUser(userId.Value, followsId);
            TempData["Message"] = "You are now following this user.";
            return RedirectToAction("SearchUsers", "Follower");
        }

        [HttpPost]
        public IActionResult Unfollow(int followsId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            _followerService.UnfollowUser(userId.Value, followsId);
            TempData["Message"] = "You have unfollowed this user.";
            return RedirectToAction("Searchusers", "Follower");
        }

        [HttpGet]
        public IActionResult Followers()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var followers = _followerService.GetFollowers(userId.Value);
            return View(followers);
        }
    }
}