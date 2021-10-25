using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ForkToFit.Models;

namespace ForkToFit.Repositories 
{
    public class MealPlanRepository : BaseRepository, IMealPlanRepository
    {

        public MealPlanRepository(IConfiguration config) : base(config) { }




        // this method gets a list of all the meal plans
        public List<MealPlan> GetAllMealPlans()
        {
            //making the sql connectioin
            using (SqlConnection conn = Connection)
            {
                //opening the command
                conn.Open();
                //creating the command
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // line below must match all the columns in your database
                    cmd.CommandText = @"
                        SELECT Id, Name, UserProfileId, MealPlanTypeId, CalorieTracker
                        FROM MealPlan
                    ";
                    // read all the values and initializing the reader
                    SqlDataReader reader = cmd.ExecuteReader();
                    // creating a variable to hold the list of displayed foods
                    List<MealPlan> mealPlans = new List<MealPlan>();
                    // using a while is necessary so that it gets everything in the list
                    while (reader.Read())
                    {
                        // creating a new instance of displayedfood
                        MealPlan mealPlan = new MealPlan
                        {
                            // storing the values returned from the reader to the corresponding properties/columns
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            MealPlanTypeId = reader.GetInt32(reader.GetOrdinal("MealPlanTypeId")),
                            CalorieTracker = reader.GetString(reader.GetOrdinal("CalorieTracker"))                          
                        };

                        mealPlans.Add(mealPlan);
                    }

                    reader.Close();

                    return mealPlans;
                }
            }
        }






    }
}
