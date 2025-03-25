using System.ComponentModel.DataAnnotations;
using ApplicationLayer;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationLayer.Dtos.User_DTOs;
using ApplicationLayer.Dtos.Order_DTOs;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserHandler _userHandler;

        public UserController(UserHandler userHandler)
        {
            _userHandler = userHandler;
        }


        [HttpGet]
        [Route("{phoneNumber}")]
        public ActionResult<GetUserDto> GetUser
        (
            [Required(ErrorMessage = "PhoneNumber is Required")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
            string phoneNumber
        )
        
        {
            if (!ModelState.IsValid)
                return BadRequest("phoneNumber is Invalid");

            Users? user = _userHandler.GetUser(phoneNumber);


            if (user == null)
            {
                return BadRequest();
            }

            GetUserDto user_dto = new GetUserDto()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };



            return Ok(user_dto);
        }

        [HttpGet]
        [Route("TryGetUserOrder/{phoneNumber}")]
        public ActionResult<RetrieveUserOrderDto> GetUserOrder(
            [Required(ErrorMessage = "PhoneNumber is Required")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
            string phoneNumber
            )
        {
            if (!ModelState.IsValid)
                return BadRequest("phoneNumber is Invalid");

            if (!_userHandler.TryGetUserOrder(phoneNumber, out RetrieveUserOrderDto? u))
            {
                return NotFound("User Has No Orders");
            }


            return u;
        }
    }
}
