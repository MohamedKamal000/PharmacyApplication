namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IUserRepository<TObject> : IRepository<TObject>
    {
        Order GetUserOrders(TObject user);
        Users? RetrieveUserCredentials(string phoneNumber);
    }
}