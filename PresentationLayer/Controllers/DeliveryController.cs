using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Delivery_Handling;
using ApplicationLayer.Dtos.Delivery_DTOs;
using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;



[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly DeliveryManHandler _deliveryManHandler;
    
    public DeliveryController(DeliveryManHandler deliveryManHandler)
    {
        _deliveryManHandler = deliveryManHandler;
    }

    [HttpGet]
    [Route("GetDeliveryManWithPhone/{phoenNumber}")]
    public ActionResult<Delivery> GetDeliveryManByPhone(
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
        string phoneNumber)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_deliveryManHandler.TrySearchDeliveryManWithPhone(phoneNumber, out Delivery? result))
        {
            return Ok(result);
        }

        return BadRequest("Something wrong Happen");
    }
    
    [HttpGet]
    [Route("GetDeliveryManWithName/{name}")]
    public ActionResult<Delivery> GetDeliveryManByName(
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name is Not Accepted")]
        string name)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_deliveryManHandler.TrySearchDeliveryManWithName(name, out Delivery? result))
        {
            return Ok(result);
        }

        return BadRequest("Something wrong Happen");
    }



    [HttpPost]
    [Route("CreateNewDeliveryMan")]
    public ActionResult<string> CreateNewDelivery(DeliveryDto deliveryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_deliveryManHandler.TryAddDeliveryMan(deliveryDto))
        {
            return Ok("Created Successfully");
        }

        return BadRequest("Something wrong happen");
    }


    [HttpPut]
    [Route("UpdateDeliveryMan/{oldPhoneNumber}")]
    public ActionResult<string> UpdateDeliveryMan(
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
        [FromRoute] string oldPhoneNumber,
        DeliveryDto deliveryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_deliveryManHandler.TryUpdateDeliveryMan(oldPhoneNumber, deliveryDto))
        {
            return Ok("Updated Successfully");
        }

        return BadRequest("Something wrong happen");
    }

    [HttpDelete]
    [Route("DeleteDeliveryMan/{phoneNumber}")]
    public ActionResult<string> DeleteDeliveryMan(
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name is Not Accepted")]
        string phoneNumber)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_deliveryManHandler.TryDeleteDeliveryMan(phoneNumber))
        {
            return Ok("Deleted Successfully");
        }

        return BadRequest("Something wrong happen");
    }
}