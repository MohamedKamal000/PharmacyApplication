using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;


namespace InfrastructureLayer.Repositories
{
    public class MedicalCategoryRepository : GenericRepository<MedicalCategory>, IMedicalCategory
    {
        public MedicalCategoryRepository(ApplicationDbContext dbConnection) : base(dbConnection)
        {
        }


        public bool IsCategoryExist(string medicalCategoryName)
        {
            bool result = false;

            try
            {
                result = _dbSet.FirstOrDefault(m => m.CategoryName == medicalCategoryName) != null;
            }
            catch(Exception e)
            {
                throw new Exception($"Failed to call function IsCategoryExist, Error {e.Message}, ErrorStack: {e.StackTrace}");

            }

            return result;
        }
    }
}