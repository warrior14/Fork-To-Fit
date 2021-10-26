using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ForkToFit.Models;

namespace ForkToFit.Repositories
{
    public class MealPlanTypeRepository : BaseRepository, IMealPlanTypeRepository
    {

        public MealPlanTypeRepository(IConfiguration config) : base(config) { }






        // this method gets a list of all the meal plan types
        public List<MealPlanType> GetAllMealPlanTypes()
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
                        SELECT Id, Name
                        FROM MealPlanType
                    ";
                    // read all the values and initializing the reader
                    SqlDataReader reader = cmd.ExecuteReader();
                    // creating a variable to hold the list of displayed foods
                    List<MealPlanType> mealPlanTypes = new List<MealPlanType>();
                    // using a while is necessary so that it gets everything in the list
                    while (reader.Read())
                    {
                        // creating a new instance of displayedfood
                        MealPlanType mealPlanType = new MealPlanType
                        {
                            // storing the values returned from the reader to the corresponding properties/columns
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };

                        mealPlanTypes.Add(mealPlanType);
                    }

                    reader.Close();

                    return mealPlanTypes;
                }
            }
        }






    }
}
