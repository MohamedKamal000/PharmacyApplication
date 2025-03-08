namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IUserRepository<TObject> : IRepository<TObject>
    {
        IEnumerable<Orders> GetUserOrders(TObject user);
        Users? RetrieveUserCredentials(string phoneNumber);
    }
}