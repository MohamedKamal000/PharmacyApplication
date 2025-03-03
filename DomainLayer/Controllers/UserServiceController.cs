using DataAccess;
using DomainLayer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DomainLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly UsersLogin _usersLogin;

        public UserServiceController(UsersLogin usersLogin)
        {
            _usersLogin = usersLogin;
        }


        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult<int> RegisterNewUser(Users user)
        {
            int resultID = _usersLogin.RegisterNewUser(user);

            if (resultID != -1)
            {
                return Ok(resultID);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{phoneNumber}")]
        public ActionResult<UserDto> GetUser(string phoneNumber)
        {
            Users user = _usersLogin.GetUser(phoneNumber);

            if (user == null)
            {
                return BadRequest();
            }

            UserDto user_dto = new UserDto()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Role = user.Role
            };

            return Ok(user_dto);
        }
    }
}
