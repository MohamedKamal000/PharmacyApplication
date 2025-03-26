using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace InfrastructureLayer.Repositories
{
    public class MedicalProductsRepository : GenericRepository<Product>, IProductRepository
    {
        public MedicalProductsRepository(ApplicationDbContext dbConnection) 
            : base(dbConnection)
        {

        }

        public ICollection<Product> GetAllProductsWithMedicalCategory(MedicalCategory medicalCategory)
        {
            ICollection<Product> products = new List<Product>();

            try
            {
                products = _dbSet
                    .Where(p => p.MedicalCategory.CategoryName == medicalCategory.CategoryName)
                    .OrderBy(p => p.ProductName)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to call function GetAllProductsWithMedicalCategory, Error {e.Message}, ErrorStack: {e.StackTrace}");

            }

            return products;
        }

        public ICollection<Product> GetAllProductsWithSubMedicalCategory(SubMedicalCategory subMedicalCategory)
        {
            ICollection<Product> products = new List<Product>();

            try
            {
                products = _dbSet
                    .Where(p => p.MedicalCategory.CategoryName == subMedicalCategory.SubMedicalCategoryName)
                    .OrderBy(p => p.ProductName)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to call function GetAllProductsWithSubMedicalCategory, Error {e.Message}, ErrorStack: {e.StackTrace}");
            }

            return products;
        }

        public Product? GetProductByName(string productName)
        {
            Product? product = null;

            try
            {
                product = _dbSet.FirstOrDefault(p => p.ProductName == productName);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to call function GetProductByName, Error {e.Message}, ErrorStack: {e.StackTrace}");

            }

            return product;
        }
    }
}