using DataAnnotationsExtensions;
using HomeMadeFood.Models;
using HomeMadeFood.Web.Common.Mapping;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SimpleIngredientViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        //[Required]
        //[MinLength(2)]
        //[MaxLength(50)]
        //public string Name { get; set; }

        //[Required]
        //[Min(0)]
        //public double Quantity { get; set; }
    }
}