﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkToFit.Models;
using ForkToFit.Repositories;
using ForkToFit.Models.ViewModels;
using System.Security.Claims;
using Microsoft.Data.SqlClient;

namespace ForkToFit.Controllers
{
    public class DisplayedFoodController : Controller
{
        private readonly IDisplayedFoodRepository _displayedFoodRepo;
        private readonly IMealPlanRepository _mealPlanRepo;
        private readonly IMealTimeRepository _mealTimeRepo;
        private readonly IDayCategoryRepository _dayCategoryRepo;


        // ASP.NET will give us an instance of our DisplayedFood Repository. This is called "Dependency Injection" 


        // this is a constructor that takes in a parameter of IDisplayedFoodRepository and the displayedFoodRepository is the variable that holds it.
        public DisplayedFoodController(IDisplayedFoodRepository displayedFoodRepository, IMealPlanRepository mealPlanRepository, IMealTimeRepository mealTimeRepository, IDayCategoryRepository dayCategoryRepository)
        {
            _displayedFoodRepo = displayedFoodRepository;
            _mealPlanRepo = mealPlanRepository;
            _mealTimeRepo = mealTimeRepository;
            _dayCategoryRepo = dayCategoryRepository;
        }




        // GET: DisplayedFoodController
        // ORIGINAL:
        //public ActionResult Index()
        //{
        //    List<DisplayedFood> displayedFoods = _displayedFoodRepo.GetAllDisplayedFoods();

        //    return View(displayedFoods);
        //}

        // VIEW MODEL:
        public ActionResult Index()
        {
            var currentUserId = GetLoggedUserProfileId();
            var dfvm = new AddFoodToMealPlanViewModel();
            dfvm.MealPlans = _mealPlanRepo.GetMealPlansByUserId(currentUserId);
            dfvm.DisplayedFoods = _displayedFoodRepo.GetAllDisplayedFoods();
            dfvm.DayCategories = _dayCategoryRepo.GetAllDayCategories();
            dfvm.MealTimes = _mealTimeRepo.GetAllMealTimes();

            //dfvm.FoodSelected = null;
            
            return View(dfvm);
            //List<DisplayedFood> displayedFoods = _displayedFoodRepo.GetAllDisplayedFoods();

            //return View(displayedFoods);
        }

        private int GetLoggedUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
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


    public ActionResult AddFoodToMealPlan(AddFoodToMealPlanViewModel vm)
        {
            try
            {
                _displayedFoodRepo.AddFoodSelectedToMealPlan(vm.FoodSelected);
                return RedirectToAction(nameof(Index));
            } catch
            {
                return View(vm);
            }

        }
}
}
