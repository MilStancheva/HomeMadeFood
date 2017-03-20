using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class AddDailyMenuViewModel : IMapFrom<DailyMenu>, IMapTo<DailyMenu>
    {
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }
        
        public IEnumerable<RecipeViewModel> Salads { get; set; }
              
        public IEnumerable<RecipeViewModel> BigSalads { get; set; }
     
        public IEnumerable<RecipeViewModel> Soups { get; set; }
    
        public IEnumerable<RecipeViewModel> MainDishes { get; set; }
        
        public IEnumerable<RecipeViewModel> Vegetarian { get; set; }
      
        public IEnumerable<RecipeViewModel> BBQ { get; set; }
       
        public IEnumerable<RecipeViewModel> Pasta { get; set; }
    }
}