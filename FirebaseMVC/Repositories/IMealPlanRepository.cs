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

    }
}
