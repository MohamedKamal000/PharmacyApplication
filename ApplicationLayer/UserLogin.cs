﻿using ApplicationLayer.Dtos.User_DTOs;
using DomainLayer;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer
{
    // should consider adding any of the following features later on 
    
    /*
        Logout // more for session management not for this class
        ForgotPassword // uses one of the user credentials to make him change his password (maybe in the future)
        ValidateSession // more for session managment
     */
    
    // Must Make The Input Validator 
    
    public class UserLogin
    {
        private readonly IUserRepository<Users> _usersGenericRepository;
        private readonly IPasswordHasher _passwordHasher;
        
        public UserLogin(IUserRepository<Users> usersGenericRepository,IPasswordHasher passwordHasher)
        {
            _usersGenericRepository = usersGenericRepository;
            _passwordHasher = passwordHasher;
        }
        
        public  bool Login(LoginUserDto userLoginDto,out IUserRole? userRole)
        {
            bool isOk = false;
            userRole = null;
            
            Users? user = _usersGenericRepository.RetrieveUserCredentials(userLoginDto.PhoneNumber);
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
            // Make Input Validator class later to check for values Entered and size of it and missing values
            if (!user.PhoneNumber.All(char.IsDigit)) return -1;

            user.Password = _passwordHasher.Hash(user.Password);
            return _usersGenericRepository.Add(user);
        }
        
        public  bool ChangePassword(IUserRole userSession, string newPassword,string oldPassword)
        {
            bool isOk = false;

            Users user = _usersGenericRepository.RetrieveUserCredentials(userSession.IdentifyUser());

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(oldPassword, user.Password)) return isOk;
            
            // User Input Validator to see the new password is empty or not and ensure the key is password
            // should check that in the beginning of the function

            user.Password = _passwordHasher.Hash(user.Password);
            int result = _usersGenericRepository.Update(
                new KeyValuePair<string, object>("PhoneNumber", userSession.IdentifyUser()),
                user);

            isOk = result != -1;

            return isOk;
        }
        public  bool DeleteAccount(IUserRole userSession, string password)
        {
            bool isOk = false;

            Users user = _usersGenericRepository.RetrieveUserCredentials(userSession.IdentifyUser());

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(password, user.Password)) return isOk;

            int result = _usersGenericRepository.Delete(new KeyValuePair<string, object>("PhoneNumber",
                userSession.IdentifyUser()));

            isOk = result != -1;
            return isOk;
        }


        public bool CheckUserExist(string phoneNumber)
        {
            return _usersGenericRepository.
                CheckExist(new KeyValuePair<string, object>("PhoneNumber", phoneNumber));
        }
    }
}