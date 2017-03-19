using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeMadeFood.Models
{
    public class DailyMenu
    {
        private ICollection<Recipe> recipes;
        private ICollection<ApplicationUser> orderedByUresers;

        public DailyMenu()
        {
            this.recipes = new HashSet<Recipe>();
            this.orderedByUresers = new HashSet<ApplicationUser>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public decimal Price { get; set; }

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

        public virtual ICollection<ApplicationUser> OrderedByUsers
        {
            get
            {
                return this.orderedByUresers;
            }
            set
            {
                this.orderedByUresers = value;
            }
        }
    }
}