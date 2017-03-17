using System.ComponentModel.DataAnnotations;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class AddFoodCategoryViewModel : IMapFrom<FoodCategory>, IMapTo<FoodCategory>
    {
        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public FoodType FoodType { get; set; }

        [Required]
        public MeasuringUnitType MeasuringUnit { get; set; }
    }
}