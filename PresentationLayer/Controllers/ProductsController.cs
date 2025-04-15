using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Dtos.Product_DTOS;
using ApplicationLayer.Products_Handling;
using DomainLayer;
using InfrastructureLayer.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Filters;
using PresentationLayer.Utilities;

namespace PresentationLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    [Authorize(Roles = "Admin")]
    public ActionResult CreateNewProduct(CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ProblemDetailsManipulator
                .CreateProblemDetailWithBadRequest("InvalidInput"));

        if (_productHandler.TryCreateProduct(productDto))
        {
            return Ok("Item Created");
        }

        return BadRequest(ProblemDetailsManipulator
            .CreateProblemDetailWithBadRequest("InvalidInput"));
    }


    [HttpPut]
    [Route("UpdateProduct")]
    // Add UpdateProductAuditHereInFuture
    [Authorize(Roles = "Admin")]
    public ActionResult UpdateProduct(
        [Required(ErrorMessage = "Invalid Input")] 
        [StringLength(maximumLength:190,MinimumLength = 20)]
        string oldProductName,
        CreateProductDto newProductDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (_productHandler.TryUpdateProduct(oldProductName, newProductDto))
        {
            return Ok("Item Updated");
        }        
        
        return  BadRequest(ProblemDetailsManipulator
            .CreateProblemDetailWithBadRequest("InvalidInput"));
    }



    [HttpDelete]
    [Route("DeleteProduct")]
    [Authorize(Roles = "Admin")] 
    public ActionResult DeleteProduct(
        [Required(ErrorMessage = "Invalid Input")] 
        [StringLength(maximumLength:190,MinimumLength = 20)]
        string productName)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (_productHandler.TryDeleteProduct(productName))
        {
            return Ok("Item Deleted");
        }
        
        return  BadRequest(ProblemDetailsManipulator
            .CreateProblemDetailWithBadRequest("InvalidInput"));
    }

    [HttpGet]
    [Route("ProductWithName/{productName}")]
    public ActionResult<Product> GetProduct(
        [Required(ErrorMessage = "Invalid Input")]
        [StringLength(maximumLength: 190, MinimumLength = 20)]
        string productName)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (_productHandler.TrySearchProductByName(productName,out Product? p))
        {
            return Ok(p);
        }
        
        return  BadRequest(ProblemDetailsManipulator
            .CreateProblemDetailWithBadRequest("InvalidInput"));
    }


    [HttpGet]
    [Route("ProductsWithCategory/{category}")]
    public ActionResult<List<Product>> GetProductsWithCategory(
        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(maximumLength:200,MinimumLength = 4,ErrorMessage = "String Length is not accepted")]
        string category)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (_productHandler.TryGetAllProductsByMainCategory(category,out List<Product> ? products))
        {
            return Ok(products);
        }
        
        return  BadRequest(ProblemDetailsManipulator
            .CreateProblemDetailWithBadRequest("InvalidInput"));
    }

    [HttpGet]
    [Route("isAvailable/{productName}")]
    public ActionResult<string> IsProductAvailable(
        [Required(ErrorMessage = "Invalid Input")]
        [StringLength(maximumLength: 190, MinimumLength = 20)]
        string productName)
    {
        if (!ModelState.IsValid) return BadRequest();

        if (_productHandler.IsProductAvailable(productName))
        {
            return Ok("Product is Available");
        }

        return NotFound("Product not Found");
    }
}