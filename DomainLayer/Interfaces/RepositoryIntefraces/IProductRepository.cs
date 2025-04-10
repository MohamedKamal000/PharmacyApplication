

namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IProductRepository : IRepository<Product>
    {
        ICollection<Product>  GetAllProductsWithMedicalCategory(MedicalCategory medicalCategory);
        ICollection<Product> GetAllProductsWithSubMedicalCategory(SubMedicalCategory subMedicalCategory);

        Product? GetProductByName(string productName);
    }
}
