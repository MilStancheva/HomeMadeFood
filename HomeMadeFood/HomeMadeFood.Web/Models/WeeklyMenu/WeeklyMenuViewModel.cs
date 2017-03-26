using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Models.WeeklyMenu
{
    public class WeeklyMenuViewModel
    {
        [Required]
        public IEnumerable<PublicDalilyMenuViewModel> DailyMenus { get; set; }
    }
}