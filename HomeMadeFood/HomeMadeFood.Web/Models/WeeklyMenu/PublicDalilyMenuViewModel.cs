using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomeMadeFood.Models;
using HomeMadeFood.Web.App_GlobalResources;
using HomeMadeFood.Web.Common.Mapping;

namespace HomeMadeFood.Web.Models.WeeklyMenu
{
    public class PublicDalilyMenuViewModel : IMapFrom<DailyMenu>, IMapTo<DailyMenu>
    {
        [Required]
        [DataType(DataType.Date)]
        public DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessageResourceName = "DateIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public DateTime Date { get; set; }

        public IEnumerable<PublicRecipeViewModel> Salads { get; set; }

        public IEnumerable<PublicRecipeViewModel> BigSalads { get; set; }

        public IEnumerable<PublicRecipeViewModel> Soups { get; set; }

        public IEnumerable<PublicRecipeViewModel> MainDishes { get; set; }

        public IEnumerable<PublicRecipeViewModel> Vegetarian { get; set; }

        public IEnumerable<PublicRecipeViewModel> BBQ { get; set; }

        public IEnumerable<PublicRecipeViewModel> Pasta { get; set; }
    }
}