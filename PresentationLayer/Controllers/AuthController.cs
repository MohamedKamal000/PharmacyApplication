using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationLayer.Dtos.User_DTOs;
using ApplicationLayer.Users_Handling;
using DomainLayer;
using DomainLayer.Interfaces;
using InfrastructureLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.Utilities;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserLogin _usersLogin;
        private readonly ITokenGenerator _tokenGenerator;
        
        public AuthController(UserLogin usersLogin,ITokenGenerator tokenGenerator)
        {
            _usersLogin = usersLogin;
            _tokenGenerator = tokenGenerator;
        }


        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult<string> RegisterNewUser(RegisterUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userIdentifier = _usersLogin.RegisterNewUser(userDto);

            if (userIdentifier != null)
            {
                return Ok(_tokenGenerator.GenerateToken(userIdentifier.IdentifyUser()));
            }

            ProblemDetails problem =
                ProblemDetailsManipulator.CreateProblemDetailWithBadRequest("Phone Number is Invalid");
            

            return BadRequest(problem);
        }


        [HttpPost]
        [Route("Login")]
        public ActionResult<string> Login(LoginUserDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var requestResult = _usersLogin.Login(userLoginDto,out IUserIdentifier? userIdentifier);

            if (!requestResult || userIdentifier == null) return BadRequest("InvalidCredentials");

            return Ok(_tokenGenerator.GenerateToken(userIdentifier.IdentifyUser()));
        }
        
    }
}
