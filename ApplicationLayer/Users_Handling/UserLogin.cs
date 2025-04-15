using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Users_Handling
{
    // should consider adding any of the following features later on 
    
    /*
        Logout // more for session management not for this class
        ForgotPassword // uses one of the user credentials to make him change his password (maybe in the future)
        ValidateSession // more for session management
     */
    
    // Must Make The Input Validator 
    
    public class UserLogin
    {
        private readonly IUserRepository<User?> _usersGenericRepository;
        private readonly IPasswordHasher _passwordHasher;
        
        public UserLogin(IUserRepository<User?> usersGenericRepository,IPasswordHasher passwordHasher)
        {
            _usersGenericRepository = usersGenericRepository;
            _passwordHasher = passwordHasher;
        }
        
        public  bool Login(LoginUserDto userLoginDto,out IUserIdentifier? userRole)
        {
            bool isOk = false;
            userRole = null;
            
            User? user = _usersGenericRepository.RetrieveUser(userLoginDto.PhoneNumber);
            if (user != null && _passwordHasher.Verify(userLoginDto.Password, user.Password))
            {
                userRole = user.Role
                    ? new AdminIdentity(user.PhoneNumber,user.UserName) 
                    : new UserIdentity(user.PhoneNumber,user.UserName);
                isOk = true;
            }
            
            
            return isOk;
        }

        public  IUserIdentifier? RegisterNewUser(RegisterUserDto userDto)
        {
            if (!userDto.PhoneNumber.All(char.IsDigit)) return null;
            if (_usersGenericRepository.CheckUserExistByPhone(userDto.PhoneNumber)) return null;


            User user = new User()
            {
                PhoneNumber = userDto.PhoneNumber,
                Password = userDto.Password,
                UserName = userDto.UserName,
                Role = false
            };
            
            user.Password = _passwordHasher.Hash(userDto.Password);
            
            UserIdentity? userIdentity = _usersGenericRepository.Add(user) != -1 ? 
                new UserIdentity(user.PhoneNumber,user.UserName) 
                : null;
            
            return userIdentity;
        }
        
        public  IUserIdentifier? ChangePassword(string userPhoneNumber, string newPassword,string oldPassword)
        {
            IUserIdentifier? isOk = null;
            
            User? user = _usersGenericRepository.RetrieveUser(userPhoneNumber);

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(oldPassword, user.Password)) return isOk;
            

            user.Password = _passwordHasher.Hash(newPassword);
           

            int result = _usersGenericRepository.Update(user);


            if (result == -1)
                isOk = null;
            else
                isOk = user.Role
                    ? new AdminIdentity(user.PhoneNumber, user.UserName)
                    : new UserIdentity(user.PhoneNumber, user.UserName);
            
            return isOk;
        }

        

        // should apply as well to the jwt token
        public bool DeleteAccount(string userPhoneNumber, string password)
        {
            bool isOk = false;

            User ? user = _usersGenericRepository.RetrieveUser(userPhoneNumber);

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(password, user.Password)) return isOk;

            int result = _usersGenericRepository.Delete(user);

            isOk = result != -1;
            return isOk;
        }

        
        
        public IUserIdentifier? TryUpdateUser(string oldPhoneNumber,UpdateUserDto updateUserDto)
        {
            IUserIdentifier? isOk = null;

            User? user = _usersGenericRepository.RetrieveUser(oldPhoneNumber);

            if (user == null) return isOk;

            user.UserName = updateUserDto.NewUserName;
            user.PhoneNumber = updateUserDto.PhoneNumber;

            int result = _usersGenericRepository.Update(user);

            if (result == -1) return isOk;
            isOk = user.Role
                ? new AdminIdentity(user.PhoneNumber, user.UserName)
                : new UserIdentity(user.PhoneNumber, user.UserName);

            return isOk;
        }
        
        
    }
}