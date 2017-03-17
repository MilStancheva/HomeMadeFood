using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DataAnnotationsExtensions;

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
        [Index]
        public string Name { get; set; }

        [Min(0)]
        public decimal PricePerMeasuringUnit { get; set; }

        [Min(0)]
        public double QuantityInMeasuringUnit { get; set; }
        
        public FoodCategory FoodCategory { get; set; }
        public virtual Guid FoodcategoryId { get; set; }

        public Recipe Recipe { get; set; }
        public virtual Guid RecipeId { get; set; }
    }
}