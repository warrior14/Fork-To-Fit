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
    public class DayCategoryRepository : BaseRepository, IDayCategoryRepository
    {

        public DayCategoryRepository(IConfiguration config) : base(config) { }

        public List<DayCategory> GetAllDayCategories()
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
                        FROM DayCategory
                    
                    ";
                    // read all the values and initializing the reader
                    SqlDataReader reader = cmd.ExecuteReader();
                    // creating a variable to hold the list of displayed foods
                    List<DayCategory> dayCategories = new List<DayCategory>();
                    // using a while is necessary so that it gets everything in the list
                    while (reader.Read())
                    {
                        // creating a new instance of displayedfood
                        DayCategory dayCategory = new DayCategory
                        {
                            // storing the values returned from the reader to the corresponding properties/columns
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };

                        dayCategories.Add(dayCategory);
                    }

                    reader.Close();

                    return dayCategories;
                }
            }
        }
    }
}
