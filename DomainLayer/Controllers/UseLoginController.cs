using DataAccess;
using DomainLayer.Dtos;
using DomainLayer.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DomainLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseLoginController : ControllerBase
    {
        private readonly UsersLogin _usersLogin;

        public UseLoginController(UsersLogin usersLogin)
        {
            _usersLogin = usersLogin;
        }


        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult<int> RegisterNewUser(RegisterUserDto user_dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (_usersLogin.CheckUserExist(user_dto.PhoneNumber)) return BadRequest("User Already Exist");


            Users user = new Users()
            {
                PhoneNumber = user_dto.PhoneNumber,
                Password = user_dto.Password,
                UserName = user_dto.UserName,
                Role = false
            };


            int resultID = _usersLogin.RegisterNewUser(user);

            if (resultID != -1)
            {
                return Ok(resultID);
            }

            ProblemDetails problem = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Input Error",
                Type = "Input Error",
                Detail = "Phone Number is Invalid"
            };

            return BadRequest(problem);
        }

        [HttpGet]
        [Route("{phoneNumber}")]
        public ActionResult<GetUserDto> GetUser(string phoneNumber)
        {
            Users? user = _usersLogin.GetUser(phoneNumber);

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


        [HttpPost]
        [Route("Login")]
        public ActionResult<IUserRole> Login([FromForm] string phoneNumber,[FromForm] string password)
        {
            var requestResult = _usersLogin.Login(phoneNumber, password, out IUserRole userRole);

            return requestResult ? Ok(userRole.GetUserRole()) : BadRequest();
        }
    }
}
