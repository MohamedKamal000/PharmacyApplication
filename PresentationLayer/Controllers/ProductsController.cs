using ApplicationLayer.Dtos.Product_DTOS;
using ApplicationLayer.Products_Handling;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Filters;
using PresentationLayer.Utilities;

namespace PresentationLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly ProductHandler _productHandler;
    
    public ProductsController(ProductHandler productHandler)
    {
        _productHandler = productHandler;
    }


    [HttpPost]
    [Route("CreateProduct")]
    [TypeFilter(typeof(CreateProductAudit))]
    public ActionResult CreateNewProduct(CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ProblemDetailsManipulator
                .CreateProblemDetailWithBadRequest("InvalidInput"));

        bool result = _productHandler.TryCreateProduct(productDto);
        
        
        return result ? Ok("Item Created") : BadRequest(ProblemDetailsManipulator
            .CreateProblemDetailWithBadRequest("InvalidInput"));
    }
    
}