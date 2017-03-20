using System;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class EditDailyMenuViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public DateTime SelectedDate { get; set; }

        public DailyMenuViewModel SelectedDailyMenuViewModel { get; set; }

        public AddDailyMenuViewModel DailyMenuViewModelWithAllRecipes { get; set; }
    }
}