using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class IngredientViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        public Guid Id { get; set; }

        [MinLength(2, ErrorMessageResourceName = "NameMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "NameMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NameIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Name { get; set; }

        [Display(Name = ("Food Category"))]
        public FoodCategory FoodCategory { get; set; }

        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }

        public Guid FoodCategoryId { get; set; }

        public Recipe Recipe { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public Guid RecipeId { get; set; }

        [Required(ErrorMessageResourceName = "PriceIsRequiredErrorMessage", ErrorMessageResourceType = (typeof(GlobalResources)))]
        [Min(0, ErrorMessageResourceName = "PriceMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name = "Price Per Measuring Unit")]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0, ErrorMessageResourceName = "IngredientQuantityMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name = "Quantity In Measuring Unit")]
        public double QuantityInMeasuringUnit { get; set; }
    }
}