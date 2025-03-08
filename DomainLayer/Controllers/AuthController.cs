using ApplicationLayer;
using ApplicationLayer.Dtos;
using DomainLayer;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserLogin _usersLogin;

        public AuthController(UserLogin usersLogin)
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


        [HttpPost]
        [Route("Login")]
        public ActionResult<IUserRole> Login(LoginUserDto userLoginDto)
        {
            var requestResult = _usersLogin.Login(userLoginDto,out IUserRole? userRole);

            if (!requestResult || userRole == null) return BadRequest();

            return Ok(userRole.GetUserRole());
        }
    }
}
