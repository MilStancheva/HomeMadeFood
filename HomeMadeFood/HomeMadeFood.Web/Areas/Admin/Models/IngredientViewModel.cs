using DataAnnotationsExtensions;
using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class IngredientViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        public Guid Id { get; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = ("Food Category"))]
        public FoodCategory FoodCategory { get; set; }

        public string Recipe { get; set; }

        [Required]
        [Min(0)]
        [Display(Name = "Price Per Measuring Unit")]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        [Display(Name = "Quantity In Measuring Unit")]
        public double QuantityInMeasuringUnit { get; set; }
    }
}