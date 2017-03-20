using Ninject.Modules;

using HomeMadeFood.Services.Common;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Web.App_Start.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMappingService>().To<MappingService>();
            this.Bind<IFoodCategoriesService>().To<FoodCategoriesService>();
            this.Bind<IIngredientsService>().To<IngredientsService>();
            this.Bind<IRecipesService>().To<RecipesService>();
            this.Bind<IUsersService>().To<UsersService>();
            this.Bind<IDailyMenuService>().To<DailyMenuService>();
        }
    }
}