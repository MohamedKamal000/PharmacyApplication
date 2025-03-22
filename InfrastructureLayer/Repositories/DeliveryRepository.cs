using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class DeliveryRepository : GenericRepository<Delivery>
    {
        public DeliveryRepository(ApplicationDbContext dbConnection) 
            : base(dbConnection)
        {
        }

      
    }
}