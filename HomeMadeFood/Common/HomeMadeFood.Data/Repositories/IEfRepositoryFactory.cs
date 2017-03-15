namespace HomeMadeFood.Data.Repositories
{
    public interface IEfRepositoryFactory
    {
        IEfRepository<T> Create<T>() where T : class;
    }
}