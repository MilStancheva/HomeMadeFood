using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class AddRecipeViewModel : IMapFrom<Recipe>, IMapTo<Recipe>
    {
        [MinLength(2, ErrorMessageResourceName = "TitleMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "TitleMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "TitleIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Title { get; set; }

        [Display(Name ="Dish Type")]
        public DishType DishType { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MinLength(10, ErrorMessageResourceName = "DescriptionMinLengthErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [DataType(DataType.MultilineText)]
        public string Describtion { get; set; }

        [Required(ErrorMessageResourceName = "InstructionIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MinLength(10, ErrorMessageResourceName = "InstructionMinLengthErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [DataType(DataType.MultilineText)]
        public string Instruction { get; set; }

        public IEnumerable<AddIngredientViewModel> Ingredients { get; set; }
    }
}