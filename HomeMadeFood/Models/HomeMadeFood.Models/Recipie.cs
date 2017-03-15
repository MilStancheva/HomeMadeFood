using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMadeFood.Models
{
    public class Recipie
    {
        private ICollection<Ingredient> ingredients;

        public Recipie()
        {
            this.ingredients = new HashSet<Ingredient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Describtion { get; set; }

        [Required]
        [MinLength(10)]
        public string Preparation { get; set; }

        [Required]
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

        public decimal CostPerPortion { get; set; }

        public double QuantityPerPortion { get; set; }
    }
}