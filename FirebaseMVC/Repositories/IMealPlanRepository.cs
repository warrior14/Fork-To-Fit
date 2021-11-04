using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkToFit.Models;


namespace ForkToFit.Repositories
{
    public interface IMealPlanRepository
    {
        List<MealPlan> GetAllMealPlans();

        void AddMealPlan(MealPlan mealPlan);

        void DeleteMealPlan(int id);

        MealPlan GetMealPlanById(int id);

        void EditMealPlan(MealPlan mealPlan);

        List<FoodSelected> GetAllMealPlanFoodsByMealPlanId(int mealPlanId);

        List<MealPlan> GetMealPlansByUserId(int userProfileId);

        //void PatchMealPlan(MealPlan mealPlan);

        List<FoodSelected> GetFoodsByDayCategoryId(int id, int mealPlanId);

        double CalculateCalories(double foodCalories, double currentMealPlanCalories);

        void UpdateCurrentCaloriesInMealPlan(double foodCalories, int mealPlanId);

        void DeleteFoodFromMealPlan(int id);

        double SubtractCalories(double foodCalories, double currentMealPlanCalories);
    }
}
