using System.ComponentModel.DataAnnotations;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationLayer.Dtos.User_DTOs;
using ApplicationLayer.Dtos.Order_DTOs;
using ApplicationLayer.Users_Handling;
using PresentationLayer.Utilities;

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
        public ActionResult<GetUserDto> GetUser(
            [Required(ErrorMessage = "PhoneNumber is Required")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
            string phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest("phoneNumber is Invalid");

            GetUserDto? user = _userHandler.GetUser(phoneNumber);


            if (user == null)
            {

                ProblemDetails problem =
                    ProblemDetailsManipulator.CreateProblemDetailWithBadRequest("Phone Number is Invalid");
                return BadRequest(problem);
            }



            return Ok(user);
        }

        [HttpGet]
        [Route("TryGetUserOrder/{phoneNumber}")]
        public ActionResult<RetrieveUserOrderDetailsDto> GetUserOrder(
            [Required(ErrorMessage = "PhoneNumber is Required")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
            string phoneNumber)
        {
            if (!ModelState.IsValid)
                return BadRequest("phoneNumber is Invalid");

            if (!_userHandler.TryGetUserOrder(phoneNumber, out RetrieveUserOrderDetailsDto? u))
            {
                return NotFound("User Has No Orders");
            }

            return u;
        }

        [HttpPost]
        [Route("AddUserOrder/{phoneNumber}")]
        public ActionResult<int> AddUserOrder(
            [Required(ErrorMessage = "PhoneNumber is Required")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
            string phoneNumber,
            List<AddOrderDto> orders)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input");

            if (!_userHandler.TryAddNewOrders(phoneNumber, orders))
            {
                ProblemDetails problem = ProblemDetailsManipulator
                    .CreateProblemDetailWithBadRequest("Invalid Order details");
                return BadRequest(problem);
            }


            return Ok();
        }



        
    }
}
