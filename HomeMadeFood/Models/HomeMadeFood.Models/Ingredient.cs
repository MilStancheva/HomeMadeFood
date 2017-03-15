using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DataAnnotationsExtensions;

using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Models
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public FoodType FoodType { get; set; }

        [Required]
        public MeasuringUnitType MeasuringUnit { get; set; }

        [Min(0)]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        public decimal Quantity { get; set; }
    }
}