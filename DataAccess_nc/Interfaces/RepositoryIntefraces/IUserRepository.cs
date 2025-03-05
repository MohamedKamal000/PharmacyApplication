namespace DataAccess
{
    public interface IUserRepository<TObject> : IRepository<TObject>
    {
        Orders GetUserOrders(TObject user);
        Users? RetrieveUserCredentials(string phoneNumber);
    }
}