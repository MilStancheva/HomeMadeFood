using HomeMadeFood.Services.Common;
using HomeMadeFood.Services.Common.Contracts;
using Ninject.Modules;

namespace HomeMadeFood.Web.App_Start.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMappingService>().To<MappingService>();
        }
    }
}