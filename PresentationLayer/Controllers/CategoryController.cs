using ApplicationLayer.Categories_Handling;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly MainCategoryHandler _mainCategoryHandler;
    private readonly SubCategoryHandler _subCategoryHandler;
    
    public CategoryController(MainCategoryHandler mainCategoryHandler, SubCategoryHandler subCategoryHandler)
    {
        _mainCategoryHandler = mainCategoryHandler;
        _subCategoryHandler = subCategoryHandler;
    }


    [HttpGet]
    [Route("api/GetMainCategories")]
    public ActionResult<List<MedicalCategory>> GetMedicalCategories()
    {
        var result = _mainCategoryHandler.TryGetAllCategories(
            out List<MedicalCategory> categories);

        return Ok(categories);
    }
    
    
    
    [HttpGet]
    [Route("api/GetSubMainCategories")]
    public ActionResult<List<SubMedicalCategory>> GetSubMainCategories()
    {
        var result = _subCategoryHandler.TryGetAllSubCategories(
            out List<SubMedicalCategory> categories);

        return Ok(categories);
    }

}