using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Categories_Handling;
using ApplicationLayer.Dtos.Category_DTOs;
using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly MainCategoryHandler _mainCategoryHandler;
    private readonly SubCategoryHandler _subCategoryHandler;
    
    public CategoryController(MainCategoryHandler mainCategoryHandler,
        SubCategoryHandler subCategoryHandler)
    {
        _mainCategoryHandler = mainCategoryHandler;
        _subCategoryHandler = subCategoryHandler;
    }

    #region Retreive
    [HttpGet]
    [Route("GetMainCategories")]
    public ActionResult<List<MedicalCategory>> GetMedicalCategories()
    {
        var result = _mainCategoryHandler.TryGetAllCategories(
            out List<MedicalCategory> categories);

        return Ok(categories);
    }

    [HttpGet]
    [Route("GetSubCategoriesOfMainCategory/{id}")]
    public ActionResult<List<SubMedicalCategory>> GetSubCategoriesOfMc(int id)
    {
        if (_mainCategoryHandler.TryGetSubCategories(id, out List<SubMedicalCategory> result))
        {
            return Ok(result);
        }

        return BadRequest("Something wrong happen, Check Id");
    }
    
    [HttpGet]
    [Route("GetSubMainCategories")]
    public ActionResult<List<SubMedicalCategory>> GetSubMainCategories()
    {
        var result = _subCategoryHandler.TryGetAllSubCategories(
            out List<SubMedicalCategory> categories);

        return Ok(categories);
    }
    #endregion
    
    #region Creation
    [HttpPost]
    [Route("CreateNewMainCategory")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> CreateNewCategory(
        [Required]
        [StringLength(maximumLength:40,MinimumLength = 4,ErrorMessage = "Invalid SubCategoryName")]
        [FromForm]
        string categoryName)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_mainCategoryHandler.TryAddNewCategory(categoryName))
        {
            return Ok("Category Created");
        }

        return BadRequest("Something wrong happen");
    }

    [HttpPost]
    [Route("CreateNewSubCategory")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> CreateNewSubCategory(AddSubCategoryDto newSubCategory)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_subCategoryHandler.TryAddNewSubCategory(newSubCategory))
        {
            return Ok("sub Category Created");
        }

        return BadRequest("Something wrong happen");
    }
    #endregion

    #region Update

    [HttpPut]
    [Route("UpdateMainCategory/{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> UpdateMainCategory(
        [FromRoute] int id,
        [FromForm]
        [StringLength(maximumLength: 40, MinimumLength = 4, ErrorMessage = "Invalid SubCategoryName")]
        string newMainCategoryName
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_mainCategoryHandler.TryUpdateCategory(id,newMainCategoryName))
        {
            return Ok("Category Updated");
        }

        return BadRequest("Something wrong happen");

    }

    [HttpPut]
    [Route("UpdateSubCategory/{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> UpdateSubCategory(
        [FromRoute] int id,
        AddSubCategoryDto newSubCategory
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_subCategoryHandler.TryUpdateSubCategory(id,newSubCategory))
        {
            return Ok("Category Updated");
        }

        return BadRequest("Something wrong happen");
    }
    
    #endregion

    #region Delete

    [HttpDelete]
    [Route("DeleteMainCategory/{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> DeleteMainCategory(int id)
    {
        if (_mainCategoryHandler.TryDeleteCategory(id))
        {
            return Ok("Category Deleted");
        }

        return BadRequest("Something wrong happen,Check Category id");
    }
    
    [HttpDelete]
    [Route("DeleteSubCategory/{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> DeleteSubCategory(int id)
    {
        if (_subCategoryHandler.TryDeleteSubCategory(id))
        {
            return Ok("SubCategory Deleted");
        }

        return BadRequest("Something wrong happen,Check Category id");
    }


    #endregion
}