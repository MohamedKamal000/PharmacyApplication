using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Dtos.Delivery_DTOs;
using ApplicationLayer.Dtos.Order_DTOs;
using ApplicationLayer.Dtos.User_DTOs;
using ApplicationLayer.Orders_Handling;
using DomainLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class OrdersController : ControllerBase
{
    private readonly OrderHandler _orderHandler;
    
    public OrdersController(OrderHandler orderHandler)
    {
        _orderHandler = orderHandler;
    }

    [HttpGet]
    [Route("ShowAllOrders")]
    public ActionResult<List<GetOrderDto>> GetAllOrders()
    {
        if (_orderHandler.TryGetAllOrders(out List<GetOrderDto> orders))
        {
            return Ok(orders);
        }

        return NotFound("No Orders");
    }

    [HttpGet]
    [Route("ShowOrdersWithStatus/{status}")]
    public ActionResult<List<GetOrderDto>> GetOrdersWithStatus(OrderStatusEnum status)
    {
        if (_orderHandler.TryGetAllOrdersWithStatus(status, out List<GetOrderDto> orders))
        {
            return Ok(orders);
        }

        return BadRequest("Something wrong happen, no orders found or wrong status code");
    }

    [HttpPut]
    [Route("AcceptUserOrder")]
    public ActionResult<string> AcceptUserOrder(AcceptOrderDto acceptOrderDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_orderHandler.TryAcceptOrder(acceptOrderDto.UserPhoneNumber,
                acceptOrderDto.DeliveryPhoneNumber))
        {
            return Ok("Accepted Successfully");
        }

        return BadRequest("Something wrong happen");
    }

    [HttpPut]
    [Route("DeclineUserOrder")]
    public ActionResult<string> DeclineUserOrder(
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
        string userPhoneNumber)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_orderHandler.TryDeclineUserOrder(userPhoneNumber))
        {
            return Ok("Declined Successfully");
        }

        return BadRequest("Something wrong happen");
    }
    
    
    [HttpPut]
    [Route("SetOrderDelivered")]
    public ActionResult<string> SetUserOrderAsDelivered(
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
        string userPhoneNumber)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (_orderHandler.TrySetOrderAsDelivered(userPhoneNumber))
        {
            return Ok("Order has been set as delivered Successfully");
        }

        return BadRequest("Something wrong happen");
    }

    [HttpDelete]
    [Route("DeleteOrder/{id}")]
    public ActionResult<string> DeleteOrder([FromRoute] int orderId)
    {
        if (_orderHandler.TryDeleteOrder(orderId))
        {
            return Ok("Order Deleted Successfully");
        }

        return BadRequest("Something wrong happen");
    }
    
}