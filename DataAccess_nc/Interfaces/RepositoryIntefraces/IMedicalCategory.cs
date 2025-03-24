

namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IMedicalCategory : IRepository<MedicalCategory>
    {
        bool IsCategoryExist(string medicalCategoryName);
    }
}
