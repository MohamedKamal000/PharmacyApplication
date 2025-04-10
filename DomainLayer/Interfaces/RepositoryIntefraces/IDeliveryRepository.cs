


namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        Delivery? SearchDeliveryManByName(string name);
        Delivery? SearchDeliveryManByPhone(string phoneNumber);
    }
}
