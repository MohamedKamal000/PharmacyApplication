using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DataAccess
{
    // should consider adding any of the following features later on 
    
    /*
        Logout // more for session management not for this class
        ForgotPassword // uses one of the user credentials to make him change his password (maybe in the future)
        ValidateSession // more for session managment
     */
    
    // Must Make The Input Validator 
    
    public class UsersLogin
    {
        private readonly ILogger _logger;
        private readonly TableManager<Users> _usersTableManager;
        private readonly IPasswordHasher _passwordHasher;
        
        UsersLogin(ILogger logger,TableManager<Users> usersTableManager,IPasswordHasher passwordHasher)
        {
            _logger = logger;
            _usersTableManager = usersTableManager;
            _passwordHasher = passwordHasher;
        }
        
        // idk but this breaks the Dependency Inversion Principle 
        private  Users RetrieveUserCredentials(string phoneNumber)
        {
            Users user = new Users();
            try
            {
                IDbConnection dbConnection = new SqlConnection(DBSettings.connectionString);

                user = dbConnection.Query<Users>(
                    DBSettings.ProceduresNames.UserLogin.ToString(),
                    new
                    {
                        PhoneNumber = phoneNumber
                    },
                    commandType: CommandType.StoredProcedure
                    ).Single();

                if (user.PhoneNumber.Length == 0)
                {
                    user = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                _logger.LogErrorMessage(e.Message,e.StackTrace);
            }
                
            return user;
        }
        public  bool Login(string phoneNumber, string password,out IUserRole userRole)
        {
            bool isOK = false;
            userRole = null;

            try
            {
                Users user = RetrieveUserCredentials(phoneNumber);
                if (user != null && _passwordHasher.Verify(password, user.Password))
                {
                    userRole = user.Role
                        ? new Admin(user.PhoneNumber) as IUserRole
                        : new User(user.PhoneNumber) as IUserRole;
                    isOK = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                _logger.LogErrorMessage(e.Message,e.StackTrace);
            }
            
            return isOK;
        }

        public  int RegisterNewUser(Users user)
        {
            // Make Input Validator class later to check for values Entered and size of it and missing values
            user.Password = _passwordHasher.Hash(user.Password);
            return _usersTableManager.InsertIntoTable(user);
        }
        
        public  bool ChangePassword(IUserRole userSession, string newPassword,string oldPassword)
        {
            bool isOk = false;

            Users user = RetrieveUserCredentials(userSession.IdentifyUser());

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(oldPassword, user.Password)) return isOk;
            
            // User Input Validator to see the new password is empty or not and ensure the key is password
            // should check that in the beginning of the function

            user.Password = _passwordHasher.Hash(user.Password);
            int result = _usersTableManager.UpdateTable(
                new KeyValuePair<string, object>("PhoneNumber", userSession.IdentifyUser()),
                user);

            isOk = result != -1;

            return isOk;
        }
        public  bool DeleteAccount(IUserRole userSession, string password)
        {
            bool isOk = false;

            Users user = RetrieveUserCredentials(userSession.IdentifyUser());

            if (user == null) return isOk;
            

            if (!_passwordHasher.Verify(password, user.Password)) return isOk;

            int result = _usersTableManager.DeleteFromTable(new KeyValuePair<string, object>("PhoneNumber",
                userSession.IdentifyUser()));

            isOk = result != -1;
            return isOk;
        }
    }
}