using DomainLayer;
using DomainLayer.Interfaces;

namespace InfrastructureLayer.Repositories
{
    public class SubMedicalCategoryRepository : GenericRepository<SubMedicalCategory>
    {
        public SubMedicalCategoryRepository(ApplicationDbContext dbConnection) : base(dbConnection)
        {
        }

       
    }
}