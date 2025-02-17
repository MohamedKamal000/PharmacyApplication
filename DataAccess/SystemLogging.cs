using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataAccess
{
    public static class DB_Logging 
    {
        public static void LogAdminBehaviour(string adminIdentity, string logMessage)
        {
            string query = @"Insert Into SystemAdminTracking(AdminPhoneIdentifier,AccessDate,BehaviourLog)
                            Values(
                            @AdminIdentifier,
                            @Date,
                            @Behaviour
                            )";

            using (SqlConnection connection = new SqlConnection(DBSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdminIdentifier", adminIdentity);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.Parameters.AddWithValue("@Behaviour", logMessage);
                    
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void LogErrorMessage(string errorMessage, string errorStack)
        {
            string query = @"Insert Into SystemErrors(ErrorMessage, ErrorStack,ErrorDate)
                            Values(
                            @ErrorMessage,
                            @ErrorStack,
                            @ErrorDate
                            )";

            using (SqlConnection connection = new SqlConnection(DBSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                    command.Parameters.AddWithValue("@ErrorDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ErrorStack", errorStack);
                    
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
        
        
    }
}