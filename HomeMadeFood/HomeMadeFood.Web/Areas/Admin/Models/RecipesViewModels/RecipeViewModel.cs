using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class RecipeViewModel : IMapFrom<Recipe>, IMapTo<Recipe>
    {
        public Guid Id { get; set; }

        [MinLength(2, ErrorMessageResourceName = "TitleMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "TitleMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "TitleIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Title { get; set; }

        [Required]
        public DishType DishType { get; set; }

        [Required(ErrorMessageResourceName = "DescriptionIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MinLength(10, ErrorMessageResourceName = "DescriptionMinLengthErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Describtion { get; set; }

        [Required(ErrorMessageResourceName = "InstructionIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MinLength(10, ErrorMessageResourceName = "InstructionMinLengthErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Instruction { get; set; }

        [Min(0)]
        public decimal CostPerPortion { get; set; }

        [Min(0)]
        public decimal PricePerPortion { get; set; }

        [Min(0)]
        public double QuantityPerPortion { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}