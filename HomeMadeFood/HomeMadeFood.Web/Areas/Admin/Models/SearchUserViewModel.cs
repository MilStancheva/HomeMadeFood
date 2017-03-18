using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchUserViewModel
    {
        [Display(Name = "Username")]
        public string Name { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}