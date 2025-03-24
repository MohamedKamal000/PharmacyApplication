using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(ApplicationDbContext dbConnection) 
            : base(dbConnection)
        {
        }


        public Delivery? SearchDeliveryManByName(string name)
        {

            Delivery? deliveryMan;
            try
            {
                deliveryMan = _dbSet.FirstOrDefault(d => d.DeliveryManName == name);
            }
            catch (Exception e)
            {

                throw new Exception($"Failed to call function SearchDeliveryManByName, Error {e.Message}, ErrorStack: {e.StackTrace}");
            }

            return deliveryMan;
        }

        public Delivery?  SearchDeliveryManByPhone(string phoneNumber)
        {
            Delivery? deliveryMan;
            try
            {
                deliveryMan = _dbSet.FirstOrDefault(d => d.PhoneNumber == phoneNumber);
            }
            catch (Exception e)
            {

                throw new Exception($"Failed to call function SearchDeliveryManByPhone, Error {e.Message}, ErrorStack: {e.StackTrace}");
            }

            return deliveryMan;
        }
    }
}