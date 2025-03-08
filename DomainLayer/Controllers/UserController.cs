using ApplicationLayer.Dtos;
using ApplicationLayer;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<GetUserDto> GetUser(string phoneNumber)
        {
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
    }
}
