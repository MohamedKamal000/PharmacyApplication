using DataAccess;
using DomainLayer.Dtos;
using DomainLayer.Filters;
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
        public ActionResult<int> RegisterNewUser([FromForm] string phoneNumber,
            [FromForm] string password,[FromForm] string userName)
        {
            Users user = new Users()
            {
                PhoneNumber = phoneNumber,
                Password = password,
                UserName = userName,
                Role = false
            };


            int resultID = _usersLogin.RegisterNewUser(user);

            if (resultID != -1)
            {
                return Ok(resultID);
            }
            
            return BadRequest();
        }

        [HttpGet]
        [Route("{phoneNumber}")]
        public ActionResult<UserDto> GetUser(string phoneNumber)
        {
            Users? user = _usersLogin.GetUser(phoneNumber);

            if (user == null)
            {
                return BadRequest();
            }

            UserDto user_dto = new UserDto()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };

            return Ok(user_dto);
        }


        [HttpPost]
        [Route("Login")]
        public ActionResult<IUserRole> Login([FromForm] string phoneNumber,[FromForm] string password)
        {
            var requestResult = _usersLogin.Login(phoneNumber, password, out IUserRole userRole);

            return requestResult ? Ok(userRole.GetUserRole()) : BadRequest();
        }
    }
}
