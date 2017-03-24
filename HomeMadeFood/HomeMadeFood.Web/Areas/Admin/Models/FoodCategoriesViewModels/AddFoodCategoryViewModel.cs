using System.ComponentModel.DataAnnotations;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class AddFoodCategoryViewModel : IMapFrom<FoodCategory>, IMapTo<FoodCategory>
    {
        [MinLength(2, ErrorMessageResourceName = "NameMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "NameMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NameIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "FoodTypeIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public FoodType FoodType { get; set; }

        [Required(ErrorMessageResourceName = "MeasuringUnitIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public MeasuringUnitType MeasuringUnit { get; set; }
    }
}