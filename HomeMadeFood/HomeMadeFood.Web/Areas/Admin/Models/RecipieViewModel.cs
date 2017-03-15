using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class RecipieViewModel : IMapFrom<Recipie>, IMapTo<Recipie>
    {
        public Guid Id { get; }

        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Describtion { get; set; }

        [Required]
        [MinLength(10)]
        public string Preparation { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Display(Name = "Cost Per Portion")]
        public decimal CostPerPortion { get; set; }

        [Display(Name = "Quantity Per Portion")]
        public double QuantityPerPortion { get; set; }
    }
}