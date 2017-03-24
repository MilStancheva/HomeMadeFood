using System;
using System.ComponentModel.DataAnnotations;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class EditDailyMenuViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "DateIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [DataType(DataType.Date)]
        public DateTime SelectedDate { get; set; }

        public DailyMenuViewModel SelectedDailyMenuViewModel { get; set; }

        public AddDailyMenuViewModel DailyMenuViewModelWithAllRecipes { get; set; }
    }
}