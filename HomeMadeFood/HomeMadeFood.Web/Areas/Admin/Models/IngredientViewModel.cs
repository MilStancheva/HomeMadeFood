using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        [MinLength(2)]
        public string Name { get; set; }

        public FoodType FoodType { get; set; }

        public MeasuringUnitType MeasuringUnit { get; set; }

        [Min(0)]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        public decimal Quantity { get; set; }
    }
}