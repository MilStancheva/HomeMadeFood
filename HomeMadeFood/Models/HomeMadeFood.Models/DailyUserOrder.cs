using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMadeFood.Models
{
    public class DailyUserOrder
    {
        private ICollection<Recipe> orderedRecipes;

        public DailyUserOrder()
        {
            this.orderedRecipes = new HashSet<Recipe>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public decimal DailyUserOrderPrice { get; set; }

        public string AdditionalRequirements { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual string UserId { get; set; }

        public virtual ICollection<Recipe> OrderedRecipes
        {
            get
            {
                return this.orderedRecipes;
            }
            set
            {
                this.orderedRecipes = value;
            }
        }

    }
}