using ForkToFit.Models.ViewModels;
using ForkToFit.Models;
using ForkToFit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            // Making a new instance of the AddFoodToMealPlanViewModel with all its empty variables
            var dfvm = new AddFoodToMealPlanViewModel();
            // Assigning values to all of the empty variables from the repos, they will no longer be empty but instead have arrays.
            dfvm.MealPlans = _mealPlanRepo.GetMealPlansByUserId(currentUserId);
            dfvm.DisplayedFoods = _displayedFoodRepo.GetAllDisplayedFoods();
            dfvm.DayCategories = _dayCategoryRepo.GetAllDayCategories();
            dfvm.MealTimes = _mealTimeRepo.GetAllMealTimes();    
            // dfvm's variables such as MealPlans now contain arrays of things that we are passing to the view.
            return View(dfvm);
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
                MealPlan currentMealPlan = _mealPlanRepo.GetMealPlanById(vm.FoodSelected.MealPlanId);
                double updatedCalories = _mealPlanRepo.CalculateCalories(vm.FoodCalories, currentMealPlan.CurrentCalories);
                _displayedFoodRepo.AddFoodSelectedToMealPlan(vm.FoodSelected);
                _mealPlanRepo.UpdateCurrentCaloriesInMealPlan(updatedCalories, vm.FoodSelected.MealPlanId);
                System.Console.WriteLine("hi");
                return RedirectToAction(nameof(Index));
            } catch
            {
                return View(vm);
            }

        }
    }


}

