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





        //public FoodSelected GetFoodDetails(int displayedFoodId)
        //{
        //    // Its going to make a query to the database to find the DisplayedFood.
        //    // It will use the displayedFoodId foreign key inside the foodSelected to search for the DisplayedFood object.
        //    // Once found and retreived this method should return the properties of that object such as:
        //    // Name, Serving Size, Calories, Description, Macro Category Id, and Image.
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //              SELECT df.Name [dfName], df.ServingSize, df.Calories, df.Description, df.MacroCategoryId, df.ImageUrl,
        //                     mc.Id, mc.Name 
        //              FROM DisplayedFood df
        //              LEFT JOIN MacroCategory mc
        //              ON df.MacroCategoryId = mc.Id
        //            WHERE df. = @displayedFoodId
        //            ";


        //            SqlDataReader reader = cmd.ExecuteReader();

        //            FoodSelected foodSelecteds = new FoodSelected();

        //            while (reader.Read())
        //                FoodSelected foodSelected = new
        //                {

        //                }
        //        }
        //    }
        //}






    }
}
