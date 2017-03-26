using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.App_GlobalResources;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Models;

namespace HomeMadeFood.Web.Models.WeeklyMenu
{
    public class PublicRecipeViewModel : IMapFrom<Recipe>, IMapTo<Recipe>
    {
        [MinLength(2, ErrorMessageResourceName = "TitleMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "TitleMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "TitleIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Title { get; set; }

        [Required]
        public DishType DishType { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MinLength(10, ErrorMessageResourceName = "DescriptionMinLengthErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Describtion { get; set; }

        [Min(0)]
        public decimal PricePerPortion { get; set; }

        [Min(0)]
        public double QuantityPerPortion { get; set; }
    }
}