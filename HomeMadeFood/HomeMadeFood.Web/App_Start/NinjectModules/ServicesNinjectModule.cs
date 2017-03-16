using HomeMadeFood.Services.Common;
using HomeMadeFood.Services.Common.Contracts;
using HomeMadeFood.Services.Data;
using HomeMadeFood.Services.Data.Contracts;
using Ninject.Modules;

namespace HomeMadeFood.Web.App_Start.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMappingService>().To<MappingService>();
            this.Bind<IIngredientsService>().To<IngredientsService>();
            this.Bind<IRecipesService>().To<RecipesService>();
        }
    }
}