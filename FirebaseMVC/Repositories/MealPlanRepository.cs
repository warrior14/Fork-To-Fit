using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ForkToFit.Models;
using ForkToFit.Utils;

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
                        SELECT mp.Id [mpId], mp.Name [mpName], mp.UserProfileId, mp.MealPlanTypeId, mp.CalorieTracker, mpt.Id [mptId], mpt.Name [mptName]
                        FROM MealPlan mp
                        LEFT JOIN MealPlanType mpt
                        ON mp.MealPlanTypeId = mpt.Id
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
                            Id = reader.GetInt32(reader.GetOrdinal("mpId")),
                            Name = reader.GetString(reader.GetOrdinal("mpName")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            MealPlanTypeId = reader.GetInt32(reader.GetOrdinal("MealPlanTypeId")),
                            CalorieTracker = DbUtils.GetNullableInt(reader, "CalorieTracker"),
                            MealPlanType = new MealPlanType
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("mptId")),
                                Name = reader.GetString(reader.GetOrdinal("mptName"))
                            }
                        };

                        mealPlans.Add(mealPlan);
                    }

                    reader.Close();

                    return mealPlans;
                }
            }
        }

  
        // connect to displayed food and also access the name of the mealplantype name
        public List<FoodSelected> GetAllMealPlanFoodsByMealPlanId(int mealPlanId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT fs.Id, fs.MealPlanId, fs.DayCategoryId, fs.MealTimeId, fs.DisplayedFoodId,
                    df.Name [dfName], df.ServingSize, df.Calories, df.Description, df.ImageUrl, 
                    mc.MacronutrientCategoryName [mcName]
                    FROM FoodSelected fs
                    LEFT JOIN DisplayedFood df
                    ON fs.DisplayedFoodId = df.Id
                    LEFT JOIN MacroCategory mc
                    ON df.MacroCategoryId = mc.Id
                    WHERE fs.MealPlanId = @mealPlan
                    ";


                    cmd.Parameters.AddWithValue("@mealPlan", mealPlanId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<FoodSelected> foodSelecteds = new List<FoodSelected>();

                    while(reader.Read())
                    {
                        FoodSelected foodSelected = new FoodSelected
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            MealPlanId = reader.GetInt32(reader.GetOrdinal("MealPlanId")),
                            DayCategoryId = reader.GetInt32(reader.GetOrdinal("DayCategoryId")),
                            MealTimeId = reader.GetInt32(reader.GetOrdinal("MealTimeId")),
                            DisplayedFoodId = reader.GetInt32(reader.GetOrdinal("DisplayedFoodId")),
                            Name = reader.GetString(reader.GetOrdinal("dfName")),
                            ServingSize = reader.GetInt32(reader.GetOrdinal("ServingSize")),
                            Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            MacronutrientCategoryName = reader.GetString(reader.GetOrdinal("mcName"))
                        };

                        //dont forget that if you make a list then you have to return
                        foodSelecteds.Add(foodSelected);
                    }

                    
                    
                    reader.Close();
                    return foodSelecteds;

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
                        SELECT mp.Id [mpId], mp.Name [mpName], mp.UserProfileId, mp.MealPlanTypeId, mp.CalorieTracker, mpt.Id [mptId], mpt.Name [mptName]
                        FROM MealPlan mp
                        LEFT JOIN MealPlanType mpt
                        ON mpt.Id = mp.MealPlanTypeId
                        WHERE mp.Id = @id
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
                                    Id = reader.GetInt32(reader.GetOrdinal("mpId")),
                                    Name = reader.GetString(reader.GetOrdinal("mpName")),
                                    UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                    MealPlanTypeId = reader.GetInt32(reader.GetOrdinal("MealPlanTypeId")),
                                    CalorieTracker = reader.GetInt32(reader.GetOrdinal("CalorieTracker")),
                                    MealPlanType = new MealPlanType()
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("mptId")),
                                        Name = reader.GetString(reader.GetOrdinal("mptName"))
                                    }
                                };
                            }

                        }
                        return mealPlan;
                    }
                    //return mealPlan;
                }
            }
        }



        public void EditMealPlan (MealPlan mealPlan)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        Update MealPlan
                        SET
                            Name = @name,
                         MealPlanTypeId = @mealPlanTypeId,
                         CalorieTracker = @calorieTracker
                        WHERE MealPlan.Id = @id";

                    cmd.Parameters.AddWithValue("@name", mealPlan.Name);
                    cmd.Parameters.AddWithValue("@id", mealPlan.Id);
                    cmd.Parameters.AddWithValue("@mealPlanTypeId", mealPlan.MealPlanTypeId);
                    cmd.Parameters.AddWithValue("@calorieTracker", mealPlan.CalorieTracker);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<MealPlan> GetMealPlansByUserId(int userProfileId)
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
                        SELECT mp.Id [mpId], mp.Name [mpName], mp.UserProfileId, mp.MealPlanTypeId, mp.CalorieTracker, mpt.Id [mptId], mpt.Name [mptName]
                        FROM MealPlan mp
                        LEFT JOIN MealPlanType mpt
                        ON mp.MealPlanTypeId = mpt.Id
                        WHERE mp.UserProfileId = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", userProfileId);
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
                            Id = reader.GetInt32(reader.GetOrdinal("mpId")),
                            Name = reader.GetString(reader.GetOrdinal("mpName")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            MealPlanTypeId = reader.GetInt32(reader.GetOrdinal("MealPlanTypeId")),
                            CalorieTracker = DbUtils.GetNullableInt(reader, "CalorieTracker"),
                            MealPlanType = new MealPlanType
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("mptId")),
                                Name = reader.GetString(reader.GetOrdinal("mptName"))
                            }
                        };

                        mealPlans.Add(mealPlan);
                    }

                    reader.Close();

                    return mealPlans;
                }
            }

        }

        public List<FoodSelected> GetFoodsByDayCategoryId(int dayId, int mealPlanId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT fs.Id, fs.MealPlanId, fs.DayCategoryId, fs.MealTimeId, fs.DisplayedFoodId,
                    df.Name [dfName], df.ServingSize, df.Calories, df.Description, df.ImageUrl, 
                    mc.MacronutrientCategoryName [mcName],
                    mt.Id [mtId], mt.Name [mtName]
                    FROM FoodSelected fs
                    LEFT JOIN DisplayedFood df
                    ON fs.DisplayedFoodId = df.Id
                    LEFT JOIN MacroCategory mc
                    ON df.MacroCategoryId = mc.Id
                    LEFT JOIN MealTime mt
                    ON fs.MealTimeId = mt.Id
                    WHERE fs.MealPlanId = @mealPlanId
                    AND fs.DayCategoryId = @dayCategoryId
                        ";


                    cmd.Parameters.AddWithValue("@dayCategoryId", dayId);
                    cmd.Parameters.AddWithValue("@mealPlanId", mealPlanId);

                    SqlDataReader reader = cmd.ExecuteReader();


                    List<FoodSelected> mealPlanFoods = new List<FoodSelected>();

                    while (reader.Read())
                    {
                        FoodSelected foodSelected = new FoodSelected
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            MealPlanId = reader.GetInt32(reader.GetOrdinal("MealPlanId")),
                            DayCategoryId = reader.GetInt32(reader.GetOrdinal("DayCategoryId")),
                            MealTimeId = reader.GetInt32(reader.GetOrdinal("MealTimeId")),
                            DisplayedFoodId = reader.GetInt32(reader.GetOrdinal("DisplayedFoodId")),
                            Name = reader.GetString(reader.GetOrdinal("dfName")),
                            ServingSize = reader.GetInt32(reader.GetOrdinal("ServingSize")),
                            Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            MacronutrientCategoryName = reader.GetString(reader.GetOrdinal("mcName")),
                            MealTime = new MealTime()
                            {
                                Name = reader.GetString(reader.GetOrdinal("mtName"))
                            }
                        };

                        //dont forget that if you make a list then you have to return
                        mealPlanFoods.Add(foodSelected);
                    }
                    //return foodSelected;
                    reader.Close();
                    return mealPlanFoods;
                }
            }
        }



        // Patch Meal Plan
        //void PatchMealPlan(MealPlan mealPlan)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                Update MealPlan
        //                SET
        //                     = @name,
        //                 MealPlanTypeId = @mealPlanTypeId,
        //                 CalorieTracker = @calorieTracker
        //                WHERE MealPlan.Id = @id";

        //            cmd.Parameters.AddWithValue("@name", mealPlan.Name);
        //            cmd.Parameters.AddWithValue("@id", mealPlan.Id);
        //            cmd.Parameters.AddWithValue("@mealPlanTypeId", mealPlan.MealPlanTypeId);
        //            cmd.Parameters.AddWithValue("@calorieTracker", mealPlan.CalorieTracker);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

    }
}
