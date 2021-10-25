using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkToFit.Models;
using ForkToFit.Repositories;


namespace ForkToFit.Controllers
{
    public class DisplayedFoodController : Controller
{

        private readonly IDisplayedFoodRepository _displayedFoodRepo;

        // ASP.NET will give us an instance of our DisplayedFood Repository. This is called "Dependency Injection" 
       

        // this is a constructor that takes in a parameter of IDisplayedFoodRepository and the displayedFoodRepository is the variable that holds it.
        public DisplayedFoodController(IDisplayedFoodRepository displayedFoodRepository)
        {
            _displayedFoodRepo = displayedFoodRepository;
        }




        // GET: DisplayedFoodController
        public ActionResult Index()
        {
            List<DisplayedFood> displayedFoods = _displayedFoodRepo.GetAllDisplayedFoods();

            return View(displayedFoods);
        }

    // GET: DisplayedFoodController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: DisplayedFoodController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: DisplayedFoodController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
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

    // GET: DisplayedFoodController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: DisplayedFoodController/Edit/5
    [HttpPost]
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

    // GET: DisplayedFoodController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: DisplayedFoodController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
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
