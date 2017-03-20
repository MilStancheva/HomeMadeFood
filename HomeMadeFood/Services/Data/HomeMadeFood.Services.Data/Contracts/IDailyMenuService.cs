using HomeMadeFood.Models;
using System;
using System.Collections.Generic;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IDailyMenuService
    {
        IEnumerable<DailyMenu> GetAllDailyMenus();

        void AddDailyMenu(DateTime date, IEnumerable<Guid> recipesIds);

        void EditDailyMenu(Guid id, DateTime date, IEnumerable<Guid> recipesIds);

        void DeleteDailyMenu(DailyMenu menu);

        DailyMenu GetDailyMenuById(Guid id);
    }
}
