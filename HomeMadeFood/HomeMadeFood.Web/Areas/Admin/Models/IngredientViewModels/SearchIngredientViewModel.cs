using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchIngredientViewModel
    {
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}