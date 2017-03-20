using System;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class IngredientViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = ("Food Category"))]
        public FoodCategory FoodCategory { get; set; }

        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }

        public Guid FoodCategoryId { get; set; }

        public Recipe Recipe { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public Guid RecipeId { get; set; }

        [Required]
        [Min(0)]
        [Display(Name = "Price Per Measuring Unit")]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        [Display(Name = "Quantity In Measuring Unit")]
        public double QuantityInMeasuringUnit { get; set; }
    }
}