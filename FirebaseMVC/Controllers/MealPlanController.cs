using ForkToFit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForkToFit.Controllers
{
    public class MealPlanController : Controller
{



        private readonly IMealPlanRepository _mealPlanRepo;

        // ASP.NET will give us an instance of our DisplayedFood Repository. This is called "Dependency Injection" 


        // this is a constructor that takes in a parameter of IDisplayedFoodRepository and the displayedFoodRepository is the variable that holds it.
        public MealPlanController(IMealPlanRepository mealPlanRepository)
        {
            _mealPlanRepo = mealPlanRepository;
        }




        // GET: MealPlanController
        public ActionResult Index()
    {

            List<MealPlan> mealPlans = _mealPlanRepo.GetAllMealPlans();

        return View(mealPlans);
    }



    // GET: MealPlanController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: MealPlanController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: MealPlanController/Create
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

    // GET: MealPlanController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: MealPlanController/Edit/5
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

    // GET: MealPlanController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: MealPlanController/Delete/5
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
