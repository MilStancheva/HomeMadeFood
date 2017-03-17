using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;


namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class RecipeViewModel : IMapFrom<Recipe>, IMapTo<Recipe>
    {
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public DishType DishType { get; set; }

        [Required]
        [MinLength(10)]
        public string Describtion { get; set; }

        [Required]
        [MinLength(10)]
        public string Instruction { get; set; }

        [Min(0)]
        public decimal CostPerPortion { get; set; }

        [Min(0)]
        public double QuantityPerPortion { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}