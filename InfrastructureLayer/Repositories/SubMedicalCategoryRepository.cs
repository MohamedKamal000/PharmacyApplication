using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class SubMedicalCategoryRepository : GenericRepository<SubMedicalCategory>, ISubMedicalCategory
    {
        public SubMedicalCategoryRepository(ApplicationDbContext dbConnection) : base(dbConnection)
        {
        }


        public bool IsSubCategoryExist(string subMedicalCategoryName)
        {
            bool result = false;

            try
            {
                result = _dbSet.FirstOrDefault(m => m.SubMedicalCategoryName == subMedicalCategoryName) != null;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to call function IsSubCategoryExist, Error {e.Message}, ErrorStack: {e.StackTrace}");
            }

            return result;
        }
    }
}