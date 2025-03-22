using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class MedicalCategoryRepository : GenericRepository<MedicalCategory>
    {
        public MedicalCategoryRepository(ApplicationDbContext dbConnection) : base(dbConnection)
        {
        }

    }
}