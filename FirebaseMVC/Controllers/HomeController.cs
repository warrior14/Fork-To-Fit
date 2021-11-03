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

        public IActionResult CalculateBmr(UserProfile up)
        {
            // calculate the bmr

            double calculateBMRForMen(double weightInPounds, double heightInInches, int ageInYears)
            {
                double firstOperation = 6.23 * weightInPounds;
                double secondOperation = 12.7 * heightInInches;
                double thirdOperation = 6.8 * ageInYears;
                double bmr = 66 + firstOperation + secondOperation - thirdOperation;
                return bmr;
            };


            double calculateBMRForWomen(double weightInPounds, double heightInInches, int ageInYears)
            {
                double firstOperation = 4.35 * weightInPounds;
                double secondOperation = 4.7 * heightInInches;
                double thirdOperation = 4.7 * ageInYears;
                double bmr = 66 + firstOperation + secondOperation - thirdOperation;
                return bmr;
            };


            double calculateBMR(double weight, double height, int age, string sex)
            {
                if (sex == "male" || sex == "Male")
                {
                    return calculateBMRForMen(weight, height, age);
                }
                else
                {
                    return calculateBMRForWomen(weight, height, age);
                }
            };

            double calculatedBmr = calculateBMR(up.Weight, up.Height, up.Age, up.Sex);

            _userProfileRepository.SetUserProfileBmrInfo(calculatedBmr, up.Id);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DisplayBmrForm()
        {

            //var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var vm = new UserProfile();

            vm.Id = userProfileId;

            return View(vm);
        }

    }
}
