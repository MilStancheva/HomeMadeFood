using System;
using System.ComponentModel.DataAnnotations;

using DataAnnotationsExtensions;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using HomeMadeFood.Web.Common.Mapping;
using HomeMadeFood.Web.App_GlobalResources;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class FoodCategoryViewModel : IMapFrom<FoodCategory>, IMapTo<FoodCategory>
    {
        public Guid Id { get; set; }

        [MinLength(2, ErrorMessageResourceName = "NameMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [MaxLength(50, ErrorMessageResourceName = "NameMaxValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "NameIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "FoodTypeIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name="Food Type")]
        public FoodType FoodType { get; set; }

        [Required(ErrorMessageResourceName = "MeasuringUnitIsRequiredErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name="Measuring Unit")]
        public MeasuringUnitType MeasuringUnit { get; set; }

        [Min(0, ErrorMessageResourceName = "CostMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name="Cost")]
        public decimal CostOfAllCategoryIngredients { get; set; }

        [Min(0, ErrorMessageResourceName = "QuantityMinValueErrorMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Display(Name="Quantity")]
        public double QuantityOfAllCategoryIngredients { get; set; }
    }
}