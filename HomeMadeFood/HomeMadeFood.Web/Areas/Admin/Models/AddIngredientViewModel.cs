using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class AddIngredientViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = ("Food Category"))]
        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }

        public Guid SelectedFoodCategoryId { get; set; }

        [Display(Name = ("Recipe"))]
        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public Guid SelectedRecipeId { get; set; }

        [Required]
        [Min(0)]
        [Display(Name = "Price Per Measuring Unit")]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        [Display(Name = "Quantity In Measuring Unit")]
        public double QuantityInMeasuringUnit { get; set; }
    }
}