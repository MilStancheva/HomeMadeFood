using HomeMadeFood.Data;
using Ninject.Modules;
using Ninject.Web.Common;

namespace HomeMadeFood.Web.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
        }
    }
}