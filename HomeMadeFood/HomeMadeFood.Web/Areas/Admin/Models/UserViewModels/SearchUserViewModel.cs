using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchUserViewModel
    {
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}