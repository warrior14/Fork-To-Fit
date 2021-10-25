using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using ForkToFit.Models;

namespace ForkToFit.Repositories
{
    public class DisplayedFoodRepository : BaseRepository, IDisplayedFoodRepository 
    {
        public DisplayedFoodRepository(IConfiguration config) : base(config) { }

        // this method gets a list of all the displayedfoods
        public List<DisplayedFood> GetAllDisplayedFoods()
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
                        SELECT Id, Name, ServingSize, Calories, Description, MacroCategoryId, ImageUrl
                        FROM DisplayedFood
                    ";
                    // read all the values and initializing the reader
                    SqlDataReader reader = cmd.ExecuteReader();
                    // creating a variable to hold the list of displayed foods
                    List<DisplayedFood> displayedFoods = new List<DisplayedFood>();
                    // using a while is necessary so that it gets everything in the list
                    while (reader.Read())
                    {
                        // creating a new instance of displayedfood
                        DisplayedFood displayedFood = new DisplayedFood
                        {
                            // storing the values returned from the reader to the corresponding properties/columns
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ServingSize = reader.GetInt32(reader.GetOrdinal("ServingSize")),
                            Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            MacroCategoryId = reader.GetInt32(reader.GetOrdinal("MacroCategoryId")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                        };

                        displayedFoods.Add(displayedFood);
                    }

                    reader.Close();

                    return displayedFoods;
                }
            }
        }





    }
}
