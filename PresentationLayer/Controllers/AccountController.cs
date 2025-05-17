using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Dtos.User_DTOs;
using ApplicationLayer.Users_Handling;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly UserLogin _userLogin;
    private readonly ITokenGenerator _tokenGenerator;
    
    public AccountController(UserLogin userLogin, ITokenGenerator tokenGenerator)
    {
        _userLogin = userLogin;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPut]
    [Route("ChangePassword")]
    public ActionResult<string> ChangePassword(
        [Required(ErrorMessage = "old Password is Required")]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "Password Length is not valid")]
        string oldPassword,
        [Required(ErrorMessage = "new Password is Required")]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "Password Length is not valid")]
        string newPassword
        )
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var userAssignedPhone = User.FindFirst("PhoneNumber")?.Value;

        if (userAssignedPhone == null) return Forbid();


        var userIdentity = _userLogin.ChangePassword(userAssignedPhone, newPassword, oldPassword);

        if (userIdentity == null) return BadRequest("Something went wrong, Can't change password");

        return Ok(_tokenGenerator.GenerateToken(userIdentity.IdentifyUser()));
    }


    [HttpPut]
    [Route("ChangeDetails")]
    public ActionResult<string> ChangeUserDetails(UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var userAssignedPhone = User.FindFirst("PhoneNumber")?.Value;
        
        if (userAssignedPhone == null) return Forbid();

        IUserIdentifier? userIdentifier = _userLogin.TryUpdateUser(userAssignedPhone, updateUserDto);

        if (userIdentifier == null) return BadRequest("Something wrong happen while changing details");


        return Ok(_tokenGenerator.GenerateToken(userIdentifier.IdentifyUser()));
    }
    
}