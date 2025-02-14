using System;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class TableManager<T> where T : Enum
    {
        public static IUserRole Login(string userName, string password)
        {
            IUserRole userRole;
            SqlConnection connection = new SqlConnection(DBSettings.connectionString);
            SqlCommand command = new SqlCommand("UserLogin", connection);

            try
            {
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                userRole = (bool)reader["Role"] ? new Admin() as IUserRole : new User() as IUserRole;

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                connection.Close();
            }


            return userRole;
        }


        public static void TestingSomething()
        {
            SqlConnection connection = new SqlConnection(DBSettings.connectionString);
            
            connection.Open();
            connection.Close();
        }
    }
}