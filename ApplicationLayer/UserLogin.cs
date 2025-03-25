using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer
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
        private readonly IUserRepository<Users?> _usersGenericRepository;
        private readonly IPasswordHasher _passwordHasher;
        
        public UserLogin(IUserRepository<Users?> usersGenericRepository,IPasswordHasher passwordHasher)
        {
            _usersGenericRepository = usersGenericRepository;
            _passwordHasher = passwordHasher;
        }
        
        public  bool Login(LoginUserDto userLoginDto,out IUserRole? userRole)
        {
            bool isOk = false;
            userRole = null;
            
            Users? user = _usersGenericRepository.RetrieveUser(userLoginDto.PhoneNumber);
            if (user != null && _passwordHasher.Verify(userLoginDto.Password, user.Password))
            {
                userRole = user.Role
                    ? new Admin(user.PhoneNumber) as IUserRole
                    : new User(user.PhoneNumber) as IUserRole;
                isOk = true;
            }
            
            
            return isOk;
        }

        public  int RegisterNewUser(Users user)
        {
            if (!user.PhoneNumber.All(char.IsDigit)) return -1;
            if (_usersGenericRepository.CheckUserExistByPhone(user.PhoneNumber)) return -1;


            user.Password = _passwordHasher.Hash(user.Password);
            return _usersGenericRepository.Add(user);
        }
        
        public  bool ChangePassword(IUserRole userSession, string newPassword,string oldPassword)
        {
            bool isOk = false;

            Users? user = _usersGenericRepository.RetrieveUser(userSession.IdentifyUser());

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(oldPassword, user.Password)) return isOk;
            

            user.Password = _passwordHasher.Hash(newPassword);
           

            int result = _usersGenericRepository.Update(user);



            isOk = result != -1;

            return isOk;
        }


        public  bool DeleteAccount(IUserRole userSession, string password)
        {
            bool isOk = false;

            Users ? user = _usersGenericRepository.RetrieveUser(userSession.IdentifyUser());

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(password, user.Password)) return isOk;

            int result = _usersGenericRepository.Delete(user);

            isOk = result != -1;
            return isOk;
        }

    }
}