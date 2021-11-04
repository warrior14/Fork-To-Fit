﻿using ForkToFit.Models;
using ForkToFit.Models.ViewModels;
using ForkToFit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ForkToFit.Controllers
{
    public class MealPlanController : Controller
    {


        private readonly IMealPlanTypeRepository _mealPlanTypeRepo;
        private readonly IMealPlanRepository _mealPlanRepo;
        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IDayCategoryRepository _dayCategoryRepo;

        // ASP.NET will give us an instance of our DisplayedFood Repository. This is called "Dependency Injection" 


        // this is a constructor that takes in a parameter of IDisplayedFoodRepository and the displayedFoodRepository is the variable that holds it.
        public MealPlanController(
            IMealPlanRepository mealPlanRepository,
            IMealPlanTypeRepository mealPlanTypeRepository,
            IUserProfileRepository userProfileRepository,
            IDayCategoryRepository dayCategoryRepository)
        {
            _mealPlanRepo = mealPlanRepository;
            _mealPlanTypeRepo = mealPlanTypeRepository;
            _userProfileRepo = userProfileRepository;
            _dayCategoryRepo = dayCategoryRepository;
        }




        // GET: MealPlanController
        public ActionResult Index()
        {
            List<MealPlan> mealPlans = _mealPlanRepo.GetAllMealPlans();
            var filteredMealPlans = mealPlans.Where(mealPlan => mealPlan.UserProfileId == GetLoggedUserProfileId());

            return View(filteredMealPlans);
        }



        // GET: MealPlanController/Details/5
        public ActionResult Details(int id)
        {
            var vm = new DayCategoryFoodSelectedViewModel();
            Console.WriteLine(id);
            vm.ListOfDays = _dayCategoryRepo.GetAllDayCategories();
            vm.MealPlanId = id;

            return View(vm);
        }


        // GET: MealPlanController/Create
        public ActionResult Create()
        {

            // storing the list of meal plan types returned from calling the getallmealplantype method from meal plan type repo
            //List<MealPlanType> mealPlanTypes = _mealPlanTypeRepo.GetAllMealPlanTypes();

            //MealPlanFormViewModel vm = new MealPlanFormViewModel()
            //{
            //    MealPlan = new MealPlan(),
            //    MealPlanTypes = mealPlanTypes
            //};

            var vm = new MealPlanFormViewModel();
            vm.MealPlanTypes = _mealPlanTypeRepo.GetAllMealPlanTypes();


            return View(vm);
        }

        // POST: MealPlanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MealPlanFormViewModel vm)
        {
            try
            {
                // use this method if you need to create,edit etc... and you need to refer something from the userprofile table
                string userProfileId = GetCurrentUserProfileId();
                //vm.MealPlan.UserProfileId = GetCurrentUserProfileId();
                var currentUser = _userProfileRepo.GetByUserProfileId(userProfileId);
                vm.MealPlan.UserProfileId = currentUser.Id;
                _mealPlanRepo.AddMealPlan(vm.MealPlan);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e);
                vm.MealPlanTypes = _mealPlanTypeRepo.GetAllMealPlanTypes();
                return View(vm);
            }
        }




        //public ActionResult Create(MealPlan mealPlan)
        //{
        //    try
        //    {
        //        mealPlan.UserProfileId = GetCurrentUserProfileId();
        //        _mealPlanRepo.AddMealPlan(mealPlan);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View(mealPlan);
        //    }
        //}





        // GET: MealPlanController/Edit/5
        public ActionResult Edit(int id)
        {
            var evm = new MealPlanFormViewModel();
            evm.MealPlanTypes = _mealPlanTypeRepo.GetAllMealPlanTypes();
            evm.MealPlan = _mealPlanRepo.GetMealPlanById(id);

            return View(evm);

        }

        // POST: MealPlanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MealPlanFormViewModel evm)
        {
            try
            {
                //evm.MealPlan = _mealPlanRepo.GetMealPlanById(id);
                _mealPlanRepo.EditMealPlan(evm.MealPlan);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(evm);
            }
        }


        // GET: MealPlanController/Delete/5
        public ActionResult Delete(int id)
        {

            MealPlan mealPlan = _mealPlanRepo.GetMealPlanById(id);
            return View(mealPlan);
        }

        // POST: MealPlanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MealPlan mealPlan)
        {
            try
            {
                //mealPlan = _mealPlanRepo.GetMealPlanById(id);
                _mealPlanRepo.DeleteMealPlan(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(mealPlan);
            }
        }



        private int GetLoggedUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        private string GetCurrentUserProfileId()
        {
            string userProfileId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userProfileId;
        }

        public ActionResult DisplayFoods(DayCategoryFoodSelectedViewModel vm)
        {
            var mealPlanFoods = _mealPlanRepo.GetFoodsByDayCategoryId(vm.DaySelectedId, vm.MealPlanId);
            return View(mealPlanFoods);
        }


        public ActionResult RemoveFoodFromMealPlan(int foodId, int mealTimeId, int mealPlanId, double foodCalories)
        {

            _mealPlanRepo.DeleteFoodFromMealPlan(foodId);

            MealPlan currentMealPlan = _mealPlanRepo.GetMealPlanById(mealPlanId);
            double updatedCalories = _mealPlanRepo.SubtractCalories(foodCalories, currentMealPlan.CurrentCalories);
            _mealPlanRepo.UpdateCurrentCaloriesInMealPlan(updatedCalories, mealPlanId);


            // This is the stuff Im sending back to the view so it can load the page with the current foods.
            DayCategoryFoodSelectedViewModel vm = new DayCategoryFoodSelectedViewModel();
            vm.DaySelectedId = mealTimeId;
            vm.MealPlanId = mealPlanId;
            

            // Im telling it to display the DisplayFoods view and I am passing the vm modal as parameters.
            return RedirectToAction(nameof(DisplayFoods), vm);
        }

    }
}

