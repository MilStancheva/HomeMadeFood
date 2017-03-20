using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DataAnnotationsExtensions;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Models
{
    public class Recipe
    {
        private ICollection<Ingredient> ingredients;
        private ICollection<DailyMenu> dailyMenus;
        private ICollection<DailyUserOrder> dailyUserOrders;

        public Recipe()
        {
            this.ingredients = new HashSet<Ingredient>();
            this.dailyMenus = new HashSet<DailyMenu>();
            this.dailyUserOrders = new HashSet<DailyUserOrder>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        public DishType DishType { get; set; }

        [Required]
        [MinLength(10)]
        public string Describtion { get; set; }

        [Required]
        [MinLength(10)]
        public string Instruction { get; set; }

        [Min(0)]
        public decimal CostPerPortion { get; set; }

        [Min(0)]
        public decimal PricePerPortion { get; set; }

        [Min(0)]
        public double QuantityPerPortion { get; set; }

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

        public virtual ICollection<DailyMenu> DailyMenus
        {
            get
            {
                return this.dailyMenus;
            }
            set
            {
                this.dailyMenus = value;
            }
        }

        public virtual ICollection<DailyUserOrder> DailyUserOrders
        {
            get
            {
                return this.dailyUserOrders;
            }
            set
            {
                this.dailyUserOrders = value;
            }
        }
    }
}