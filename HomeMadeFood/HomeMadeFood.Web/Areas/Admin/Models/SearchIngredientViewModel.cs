using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchIngredientViewModel
    {
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}