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
        private static Users RetrieveUserCredentials(string phoneNumber)
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
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
            }
                
            return user;
        }
        public static bool Login(string phoneNumber, string password,out IUserRole userRole)
        {
            bool isOK = false;
            userRole = null;

            try
            {
                Users user = RetrieveUserCredentials(phoneNumber);
                PasswordHasher passwordHasher = new PasswordHasher();
                if (user != null && passwordHasher.Verify(password, user.Password))
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
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
            }
            
            return isOK;
        }

        public static int RegisterNewUser(Users user)
        {
            // Make Input Validator class later to check for values Entered and size of it and missing values
            PasswordHasher passwordHasher = new PasswordHasher();
            user.Password = passwordHasher.Hash(user.Password);
            return TableManager<Users>.InsertIntoTable(user);
        }
        
        public static bool ChangePassword(IUserRole userSession, string newPassword,string oldPassword)
        {
            bool isOk = false;

            Users user = RetrieveUserCredentials(userSession.IdentifyUser());

            if (user == null) return isOk;


            PasswordHasher passwordHasher = new PasswordHasher();

            if (!passwordHasher.Verify(oldPassword, user.Password)) return isOk;
            
            // User Input Validator to see the new password is empty or not and ensure the key is password
            // should check that in the beginning of the function

            user.Password = passwordHasher.Hash(user.Password);
            int result = TableManager<Users>.UpdateTable(
                new KeyValuePair<string, object>("PhoneNumber", userSession.IdentifyUser()),
                user);

            isOk = result != -1;

            return isOk;
        }
        public static bool DeleteAccount(IUserRole userSession, string password)
        {
            bool isOk = false;

            Users user = RetrieveUserCredentials(userSession.IdentifyUser());

            if (user == null) return isOk;

            PasswordHasher passwordHasher = new PasswordHasher();

            if (!passwordHasher.Verify(password, user.Password)) return isOk;

            int result = TableManager<Users>.DeleteFromTable(new KeyValuePair<string, object>("PhoneNumber",
                userSession.IdentifyUser()));

            isOk = result != -1;
            return isOk;
        }
    }
}