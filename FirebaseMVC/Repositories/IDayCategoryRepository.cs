using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkToFit.Models;


namespace ForkToFit.Repositories
{
    public interface IDayCategoryRepository
    {
        List<DayCategory> GetAllDayCategories();
    }   
}
