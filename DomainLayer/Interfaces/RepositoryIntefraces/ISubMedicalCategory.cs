

namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface ISubMedicalCategory : IRepository<SubMedicalCategory>
    {
        bool IsSubCategoryExist(string subMedicalCategoryName);
    }
}
