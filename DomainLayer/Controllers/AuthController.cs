using ApplicationLayer.Dtos.User_DTOs;
using ApplicationLayer.Users_Handling;
using DomainLayer;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Utilities;

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

            int resultID = _usersLogin.RegisterNewUser(user_dto);

            if (resultID != -1)
            {
                return Ok(resultID);
            }

            ProblemDetails problem =
                ProblemDetailsManipulator.CreateProblemDetailWithBadRequest("Phone Number is Invalid");
            

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
