using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class MedicalProductsRepository : GenericRepository<Product>
    {
        public MedicalProductsRepository(IConnection dbConnection) 
            : base(dbConnection)
        {
        }

    }
}