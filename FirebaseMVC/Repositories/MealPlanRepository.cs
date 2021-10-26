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
                            CalorieTracker = reader.GetInt32(reader.GetOrdinal("CalorieTracker"))                          
                        };

                        mealPlans.Add(mealPlan);
                    }

                    reader.Close();

                    return mealPlans;
                }
            }
        }





        public void AddMealPlan(MealPlan mealPlan)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO MealPlan ([Name], UserProfileId, MealPlanTypeId, CalorieTracker)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @userProfileId, @mealPlanTypeId, @calorieTracker);
                    ";

                    cmd.Parameters.AddWithValue("@name", mealPlan.Name);
                    cmd.Parameters.AddWithValue("@userProfileId", mealPlan.UserProfileId);
                    cmd.Parameters.AddWithValue("@mealPlanTypeId", mealPlan.MealPlanTypeId);
                    cmd.Parameters.AddWithValue("@calorieTracker", mealPlan.CalorieTracker);

                    mealPlan.Id = (int)cmd.ExecuteScalar();

                }
            }
        }






        // Delete :
        public void DeleteMealPlan(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM MealPlan
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public MealPlan GetMealPlanById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id
                        FROM MealPlan
                        WHERE Id = @id
                        ";


                    cmd.Parameters.AddWithValue("@id", id);
                    MealPlan mealPlan = null;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (mealPlan == null)
                            {
                                mealPlan = new MealPlan
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id"))
                                };
                            }

                        }
                    }
                    return mealPlan;
                }
            }
        }






    }
}
