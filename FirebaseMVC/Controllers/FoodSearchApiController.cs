using ForkToFit.Models.ViewModels;
using ForkToFit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using ForkToFit.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System;

namespace ForkToFit.Controllers
{
    public class FoodSearchApiController : Controller
    {

        public static HttpClient InitializeClient(bool checker)
        {
            //var uri = "https://superhero-search.p.rapidapi.com/api/?hero=scarlet witch";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            if (checker)
            {
                client.DefaultRequestHeaders.Add("x-rapidapi-host", "superhero-search.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("x-rapidapi-key", "e12f972de0msh5f3353dde969f09p1be95bjsna98e93807839");
            }
            else
            {
                client.DefaultRequestHeaders.Add("Referer", "https://developer.marvel.com/");
            }
            // Set request header to accept JSON format
            client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


        // POST: FoodSearchApiController/Edit/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

}
