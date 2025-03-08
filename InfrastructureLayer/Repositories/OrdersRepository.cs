using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class OrdersRepository : GenericRepository<Orders>
    {
        public OrdersRepository(IConnection dbConnection) : base(dbConnection)
        {
        }

    }
}