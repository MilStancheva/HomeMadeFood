using HomeMadeFood.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchIngredientViewModel
    {
        public SearchIngredientViewModel()
        {
            this.Page = 1;
            this.PageSize = 5;
            this.Sort = "Name";
            this.SortDir = "DESC";
        }

        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string Sort { get; set; }

        public string SortDir { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}