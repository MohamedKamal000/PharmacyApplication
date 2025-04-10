namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IExtendedRepository<TObject> : IRepository<TObject>
    {
        IEnumerable<TObject>? GetAll();
    }
}