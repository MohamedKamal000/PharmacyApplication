using System.Security.Claims;

namespace DomainLayer.Interfaces
{
    public enum SystemRoles
    {
        User,
        Admin
    }
    
    public interface IUserIdentifier
    { 
        ClaimsIdentity IdentifyUser();
    }

    // this for session management later on, idk if possible or not but we will see 
    public class UserIdentity : IUserIdentifier
    {
        private readonly string _userPhone;
        private readonly string _userName;
        
        public UserIdentity(string userPhone,string userName)
        {
            _userPhone = userPhone;
            _userName = userName;
        }
        
        public ClaimsIdentity IdentifyUser()
        {
            return new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _userName),
                new Claim("PhoneNumber", _userPhone),
                new Claim(ClaimTypes.Role, SystemRoles.User.ToString())
            });
        }
        
    }

    public class AdminIdentity : IUserIdentifier
    {
        private readonly string _userPhone;
        private readonly string _userName;

        public AdminIdentity(string userPhone,string userName)
        {
            _userPhone = userPhone;
            _userName = userName;
        }

        public ClaimsIdentity IdentifyUser()
        {
            return new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _userName),
                new Claim("PhoneNumber", _userPhone),
                new Claim(ClaimTypes.Role, SystemRoles.Admin.ToString())
            });        }
        
    }
}