using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class MedicalProductsRepository : GenericRepository<MedicalProducts>
    {
        public MedicalProductsRepository(IConnection dbConnection) 
            : base(dbConnection)
        {
        }

    }
}