
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Categories_Handling
{
    public class MainCategoryHandler
    {
        private readonly IMedicalCategory _medicalCategoryRepository;

        public MainCategoryHandler(IMedicalCategory medicalCategoryRepository)
        {
            _medicalCategoryRepository = medicalCategoryRepository;
        }

        // add, delete, GetAll, update

        public bool TryAddNewCategory(string categoryName)
        {
            if (_medicalCategoryRepository.IsCategoryExist(categoryName)) return false;


            MedicalCategory medicalCategory = new MedicalCategory() { CategoryName = categoryName };


            return _medicalCategoryRepository.Add(medicalCategory) != -1;
        }

        public bool TryDeleteCategory(int id)
        {
            if (!_medicalCategoryRepository.CheckExist(id)) return false;

            MedicalCategory medicalCategory = _medicalCategoryRepository.GetById(id)!;

            return _medicalCategoryRepository.Delete(medicalCategory) != -1;
        }

        public bool TryGetAllCategories(out List<MedicalCategory> medicalCategories)
        {
            medicalCategories = _medicalCategoryRepository.GetAll().ToList();
            return true; // idk it's just a true right ?
        }

        public bool TryUpdateCategory(int id, string newCategoryName)
        {
            if (!_medicalCategoryRepository.CheckExist(id)) return false;

            MedicalCategory medicalCategory = _medicalCategoryRepository.GetById(id)!;

            medicalCategory.CategoryName = newCategoryName;

            return _medicalCategoryRepository.Update(medicalCategory) != -1;
        }


        public bool TryGetSubCategories(int id,out List<SubMedicalCategory> subMedicalCategories)
        {
            subMedicalCategories = new List<SubMedicalCategory>();
            if (!_medicalCategoryRepository.CheckExist(id)) return false;

            MedicalCategory medicalCategory = _medicalCategoryRepository.GetById(id)!;

            subMedicalCategories = _medicalCategoryRepository.GetSubCategories(medicalCategory).ToList();

            return subMedicalCategories.Count > 0;
        }
    }
}
