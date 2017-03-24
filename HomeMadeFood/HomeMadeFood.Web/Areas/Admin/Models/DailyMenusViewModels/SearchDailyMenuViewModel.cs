using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchDailyMenuViewModel
    {
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        [Display(Name = "Daily Menus")]
        public IEnumerable<DailyMenuViewModel> DailyMenus { get; set; }
    }
}