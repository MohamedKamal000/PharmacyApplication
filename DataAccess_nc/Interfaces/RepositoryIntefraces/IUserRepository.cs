namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IUserRepository<TObject> : IRepository<TObject>
    {
        ICollection<Order> GetUserOrders(TObject user);
        Users? RetrieveUser(string phoneNumber);

        int AddOrders(List<Order> orders);

        bool CheckUserExistByPhone(string phoneNumber);
    }
}