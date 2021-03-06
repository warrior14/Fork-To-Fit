using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ForkToFit.Auth.Models;
using ForkToFit.Repositories;
using ForkToFit.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace ForkToFit.Auth
{
    public class AccountController : Controller
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IUserProfileRepository _userProfileRepository;

        public AccountController(IFirebaseAuthService firebaseAuthService, IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _firebaseAuthService = firebaseAuthService;
        }

        public IActionResult Login()
        {
            //returns empty room
            return View();
        }
        // method names determines what gets called
        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            // Authenticate user, if user's credentials are valid.
            var fbUser = await _firebaseAuthService.Login(credentials);
            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(credentials);
            }

            // if user credentials are valid it goes here, checks if the user is in the database.
            var userProfile = _userProfileRepository.GetByFirebaseUserId(fbUser.FirebaseUserId);
            if (userProfile == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to Login.");
                return View(credentials);
            }

            if (userProfile.BmrInfo == 0)
            {
                await LoginToApp(userProfile);
                return RedirectToAction("DisplayBmrForm", "Home");
            } else
            {
                await LoginToApp(userProfile);
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            // This is waiting for the register method in FirebaseAuthService.cs to complete registration
            var fbUser = await _firebaseAuthService.Register(registration);

            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to register, do you already have an account?");
                return View(registration);
            }

            var newUserProfile = new UserProfile
            {
                Email = fbUser.Email,
                FirebaseUserId = fbUser.FirebaseUserId,
                Name = registration.Name,
                DateCreated = DateTime.Now
            };
            _userProfileRepository.Add(newUserProfile);

            await LoginToApp(newUserProfile);

            return RedirectToAction("DisplayBmrForm", "Home");
        }

        // user gets logged out
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task LoginToApp(UserProfile userProfile)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                new Claim(ClaimTypes.Email, userProfile.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // where they log in to the app
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
