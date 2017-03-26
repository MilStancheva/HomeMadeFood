using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HomeMadeFood.Models.Enums;
using DataAnnotationsExtensions;

namespace HomeMadeFood.Models
{
    public class FoodCategory
    {
        private ICollection<Ingredient> ingredients;

        public FoodCategory()
        {
            this.ingredients = new HashSet<Ingredient>();
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
        public decimal CostOfAllCategoryIngredients { get; set; }

        [Min(0)]
        public double QuantityOfAllCategoryIngredients { get; set; }

        public virtual ICollection<Ingredient> Ingredients
        {
            get
            {
                return this.ingredients;
            }
            set
            {
                this.ingredients = value;
            }
        }
    }
}