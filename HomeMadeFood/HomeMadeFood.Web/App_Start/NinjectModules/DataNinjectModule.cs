using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

using HomeMadeFood.Data;
using HomeMadeFood.Data.Data;
using HomeMadeFood.Data.Repositories;

namespace HomeMadeFood.Web.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            this.Bind<IHomeMadeFoodData>().To<HomeMadeFoodData>().InRequestScope();

            this.Bind<IEfRepositoryFactory>().ToFactory().InSingletonScope();
        }
    }
}