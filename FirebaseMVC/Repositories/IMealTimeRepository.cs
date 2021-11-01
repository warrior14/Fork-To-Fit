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
    public interface IMealTimeRepository
    {
        List<MealTime> GetAllMealTimes();
    }
}
