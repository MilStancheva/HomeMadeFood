using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class AddIngredientViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        [MinLength(2, ErrorMessageResourceName = "NameMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "NameMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NameIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Name { get; set; }

        [Display(Name = ("Food Category"))]
        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }

        public Guid SelectedFoodCategoryId { get; set; }

        [Display(Name = ("Recipe"))]
        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public Guid SelectedRecipeId { get; set; }

        [Required(ErrorMessageResourceName = "PriceIsRequiredErrorMessage", ErrorMessageResourceType =(typeof(GlobalResources)))]
        [Min(0, ErrorMessageResourceName = "PriceMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name = "Price Per Measuring Unit")]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0, ErrorMessageResourceName = "IngredientQuantityMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name = "Quantity In Measuring Unit")]
        public double QuantityInMeasuringUnit { get; set; }
    }
}