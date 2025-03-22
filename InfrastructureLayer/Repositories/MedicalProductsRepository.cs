using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class MedicalProductsRepository : GenericRepository<Product>
    {
        public MedicalProductsRepository(ApplicationDbContext dbConnection) 
            : base(dbConnection)
        {
        }

    }
}