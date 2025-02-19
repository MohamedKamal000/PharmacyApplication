using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
        private static DataTable RetrieveUserCredentials(string phoneNumber)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(DBSettings.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(DBSettings.ProceduresNames.UserLogin.ToString(),
                               connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
            }
                
            return dt;
        }
        public static bool Login(string phoneNumber, string password,out IUserRole userRole)
        {
            bool isOK = false;
            userRole = null;

            try
            {
                using (DataTable dt = RetrieveUserCredentials(phoneNumber))
                {
                    PasswordHasher passwordHasher = new PasswordHasher();
                    if (dt.Rows.Count != 0 && passwordHasher.Verify(
                            password, (string)dt.Rows[0]["Password"]))
                    {
                        userRole = (bool)dt.Rows[0]["Role"]
                            ? new Admin((string)dt.Rows[0]["PhoneNumber"]) as IUserRole
                            : new User((string)dt.Rows[0]["PhoneNumber"]) as IUserRole;
                        isOK = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
            }
            
            return isOK;
        }

        public static int RegisterNewUser(Dictionary<Users, object> values)
        {
            // Make Input Validator class later to check for values Entered and size of it and missing values
            PasswordHasher passwordHasher = new PasswordHasher();
            values[Users.Password] = passwordHasher.Hash((string) values[Users.Password]);
            return TableManager<Users>.InsertIntoTable(values);
        }
        public static bool ChangePassword(KeyValuePair<Users, string> newPassword, IUserRole user, string oldPassword)
        {
            bool isOk = false;

            DataTable dt = RetrieveUserCredentials(user.IdentifyUser());

            if (dt.Columns.Count == 0) return isOk;


            PasswordHasher passwordHasher = new PasswordHasher();

            if (!passwordHasher.Verify(oldPassword, (string)dt.Rows[0]["Password"])) return isOk;
            
            // User Input Validator to see the new password is empty or not and ensure the key is password
            // should check that in the beginning of the function

            int result = TableManager<Users>.UpdateTable(
                new KeyValuePair<Users, object>(Users.PhoneNumber, user.IdentifyUser()),
                new Dictionary<Users,object>()
                {
                    { newPassword.Key, newPassword.Value }
                } );

            isOk = result != -1;

            return isOk;
        }
        public static bool DeleteAccount(IUserRole user, string password)
        {
            bool isOk = false;

            DataTable dt = RetrieveUserCredentials(user.IdentifyUser());

            if (dt.Columns.Count == 0) return isOk;

            PasswordHasher passwordHasher = new PasswordHasher();

            if (!passwordHasher.Verify(password, (string)dt.Rows[0]["Password"])) return isOk;

            int result = TableManager<Users>.DeleteFromTable(new KeyValuePair<Users, object>(Users.PhoneNumber,
                user.IdentifyUser()));

            isOk = result != -1;
            return isOk;
        }
    }
}