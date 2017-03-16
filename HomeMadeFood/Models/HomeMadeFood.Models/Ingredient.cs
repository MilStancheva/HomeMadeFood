using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DataAnnotationsExtensions;

using HomeMadeFood.Models.Enums;
using System.Collections.Generic;

namespace HomeMadeFood.Models
{
    public class Ingredient
    {
        private ICollection<Recipe> recipes;

        public Ingredient()
        {
            this.recipes = new HashSet<Recipe>();
        }

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

        public virtual ICollection<Recipe> Recipes
        {
            get
            {
                return this.recipes;
            }
            set
            {
                this.recipes = value;
            }
        }
    }
}