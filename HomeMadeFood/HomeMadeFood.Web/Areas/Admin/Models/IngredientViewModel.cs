﻿using DataAnnotationsExtensions;
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
        [Display(Name = "Food Type")]
        public FoodType FoodType { get; set; }

        [Required]
        [Display(Name = "Measuring Unit")]
        public MeasuringUnitType MeasuringUnit { get; set; }

        [Required]
        [Min(0)]
        [Display(Name = "Price Per Measuring Unit")]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        public decimal Quantity { get; set; }
    }
}