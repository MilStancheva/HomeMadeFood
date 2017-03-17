using System;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class FoodCategoryViewModel : IMapFrom<FoodCategory>, IMapTo<FoodCategory>
    {
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Food Type")]
        public FoodType FoodType { get; set; }

        [Required]
        [Display(Name="Measuring Unit")]
        public MeasuringUnitType MeasuringUnit { get; set; }

        [Min(0)]
        [Display(Name="Cost")]
        public decimal CostOfAllCategoryIngredients { get; set; }

        [Min(0)]
        [Display(Name="Quantity")]
        public double QuantityOfAllCategoryIngredients { get; set; }
    }
}