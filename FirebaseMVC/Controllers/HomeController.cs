using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ForkToFit.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ForkToFit.Repositories;
using System;

namespace ForkToFit.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public HomeController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index()
        {
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userProfile = _userProfileRepository.GetById(userProfileId);
            //userProfile.DateCreated = DateTime.Now;
            return View(userProfile);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
