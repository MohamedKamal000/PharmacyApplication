namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IUserRepository<TObject> : IRepository<TObject>
    {
        UserOder GetUserOrders(TObject user);
        Users? RetrieveUserCredentials(string phoneNumber);
    }
}