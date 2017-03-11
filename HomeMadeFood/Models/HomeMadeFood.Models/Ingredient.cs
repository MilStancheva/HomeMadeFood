using DataAnnotationsExtensions;
using HomeMadeFood.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMadeFood.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public FoodType FoodType { get; set; }

        public MeasuringUnitType MeasuringUnit { get; set; }

        [Min(0)]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        public decimal Quantity { get; set; }
    }
}