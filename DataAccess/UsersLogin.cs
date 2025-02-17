using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    // should consider adding any of the following features later on 
    
    /*
         Register     
        ChangePassword
        Logout
        ForgotPassword
        CheckIfUserExists
        ValidateSession
        DeleteAccount
        LockUserAccount
        UnlockUserAccount
        Logging
        Audit
        User Profile Management (UpdateProfile, GetUserDetails)
     */
    
    public static class UsersLogin
    {
        public static bool Login(string phoneNumber, string password,out IUserRole userRole)
        {
            bool isOK = false;
            userRole = null;

            using (SqlConnection connection = new SqlConnection(DBSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(DBSettings.ProceduresNames.UserLogin.ToString(),
                           connection))
                {
                    PasswordHasher passwordHasher = new PasswordHasher();
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && passwordHasher.Verify(
                               password, (string)reader["Password"]))
                        {
                            userRole = (bool)reader["Role"] ? new Admin((string) reader["PhoneNumber"]) as IUserRole 
                                : new User((string) reader["PhoneNumber"]) as IUserRole;
                            isOK = true;
                        }
                    }
                    
                }
            }

            return isOK;
        }
    }
}