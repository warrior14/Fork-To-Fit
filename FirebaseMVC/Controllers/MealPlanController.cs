using ForkToFit.Models;
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

        // ASP.NET will give us an instance of our DisplayedFood Repository. This is called "Dependency Injection" 


        // this is a constructor that takes in a parameter of IDisplayedFoodRepository and the displayedFoodRepository is the variable that holds it.
        public MealPlanController(
            IMealPlanRepository mealPlanRepository, 
            IMealPlanTypeRepository mealPlanTypeRepository,
            IUserProfileRepository userProfileRepository)
        {
            _mealPlanRepo = mealPlanRepository;
            _mealPlanTypeRepo = mealPlanTypeRepository;
            _userProfileRepo = userProfileRepository;
        }




        // GET: MealPlanController
        public ActionResult Index()
        {
            //var vm = new MealPlanListViewModel();

            //vm.MealPlans = _mealPlanRepo.GetAllMealPlans();

            //return View(vm);
            var mealPlans = _mealPlanRepo.GetAllMealPlans();

            return View(mealPlans);
        }



    // GET: MealPlanController/Details/5
    public ActionResult Details(int id)
    {

        List<FoodSelected> foodSelecteds = _mealPlanRepo.GetAllMealPlanFoodsByMealPlanId(id);
        return View(foodSelecteds);
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
        catch
        {
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
        //public ActionResult Edit(int id)
        //{

        //    var evm = new EditMealPlanFormViewModel();
        //    evm.MealPlanTypes = _mealPlanTypeRepo.GetAllMealPlanTypes();
        //    return View(evm);
        //}

    // POST: MealPlanController/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit(int id, EditMealPlanFormViewModel evm)
    //{
    //    try
    //    {
    //        _mealPlanRepo.EditMealPlan(evm.MealPlan);
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View(evm);
    //    }
    //}

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



        //private int GetCurrentUserProfileId()
        //{
        //    string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    return int.Parse(id);
        //}

        private string GetCurrentUserProfileId()
        {
            string userProfileId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userProfileId;
        }



    }
}

