
using ApplicationLayer.Dtos.Category_DTOs;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Categories_Handling
{
    public class SubCategoryHandler
    {
        private readonly ISubMedicalCategory _subMedicalCategory;
        private readonly IMedicalCategory _mainMedicalCategory;

        public SubCategoryHandler(ISubMedicalCategory subMedicalCategory, 
            IMedicalCategory mainMedicalCategory)
        {
            _subMedicalCategory = subMedicalCategory;
            _mainMedicalCategory = mainMedicalCategory;
        }

        public bool TryAddNewSubCategory(AddSubCategoryDto subCategory)
        {
            if (_subMedicalCategory.IsSubCategoryExist(subCategory.SubCategoryName)) return false;
            if (!_mainMedicalCategory.CheckExist(subCategory.MainCategoryId)) return false;

            SubMedicalCategory subMedicalCategory = new SubMedicalCategory()
            {
                SubMedicalCategoryName = subCategory.SubCategoryName,
                MainCategoryId = subCategory.MainCategoryId
            };


            return _subMedicalCategory.Add(subMedicalCategory) != -1;
        }

        public bool TryDeleteSubCategory(int id)
        {
            if (!_subMedicalCategory.CheckExist(id)) return false;

            SubMedicalCategory subMedicalCategory = _subMedicalCategory.GetById(id)!;

            return _subMedicalCategory.Delete(subMedicalCategory) != -1;
        }

        public bool TryGetAllSubCategories(out List<SubMedicalCategory> medicalCategories)
        {
            medicalCategories = _subMedicalCategory.GetAll().ToList();
            return true;
        }

        public bool TryUpdateSubCategory(int id, AddSubCategoryDto newSubCategoryName)
        {
            if (!_subMedicalCategory.CheckExist(id)) return false;
            if (!_mainMedicalCategory.CheckExist(newSubCategoryName.MainCategoryId)) return false;

            SubMedicalCategory subMedicalCategory = _subMedicalCategory.GetById(id)!;

            subMedicalCategory.SubMedicalCategoryName = newSubCategoryName.SubCategoryName;
            subMedicalCategory.MainCategoryId = newSubCategoryName.MainCategoryId;

            return _subMedicalCategory.Update(subMedicalCategory) != -1;
        }


    }
}
