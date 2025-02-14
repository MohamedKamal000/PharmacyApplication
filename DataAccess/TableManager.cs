using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DataAccess
{
    public class TableManager<T> where T : Enum
    {
        // used only in Insertion
        private static string ToSqlTypeConvertor(object value)
        {
            if (value == null) return "NULL";

            switch (value)
            {
                case string s:
                    return "'" + s.Replace("'", "''") + "'"; // Escape single quotes
                case DateTime dt:
                    return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'"; // Format date
                case bool b:
                    return b ? "1" : "0"; // Convert bool to bit
                default:
                    return value.ToString(); // Numbers and other types
            }

        } 
        
        public static IUserRole Login(string userName, string password)
        {
            IUserRole userRole;
            SqlConnection connection = new SqlConnection(DBSettings.connectionString);
            SqlCommand command = new SqlCommand(DBSettings.ProceduresNames.UserLogin.ToString(),connection);

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

        public static int InsertIntoTable(Dictionary<T, object> attributes)
        {
            int resultID = -1;
            SqlConnection connection = new SqlConnection(DBSettings.connectionString);

            string table = typeof(T).Name;
            string columns = string.Join(",",attributes.Keys);
            string values = string.Join(",", attributes.Values.Select(ToSqlTypeConvertor));

            SqlCommand command = new SqlCommand(DBSettings.ProceduresNames.InsertIntoAnyTable.ToString()
                , connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TableName", table);
            command.Parameters.AddWithValue("@Columns", columns);
            command.Parameters.AddWithValue("@Values", values);
            
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int id))
                {
                    resultID = id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return resultID;
        }
        
        public static bool SelectFromTable(KeyValuePair<T, object> whereClauseConstruction, out DataTable dt)
        {
            bool isOK = false;
            SqlConnection connection = new SqlConnection(DBSettings.connectionString);

            string table = typeof(T).Name;
            string column = whereClauseConstruction.Key.ToString();
            SqlCommand command = new SqlCommand(DBSettings.ProceduresNames.SelectFromTable.ToString(), connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TableName", table);
            command.Parameters.AddWithValue("@ColumnName", column);
            if (whereClauseConstruction.Value == null)
            {
                 command.Parameters.AddWithValue("@InputValue",DBNull.Value);
            }
            else
            {
                string value = whereClauseConstruction.Value.ToString();
                command.Parameters.AddWithValue("@InputValue",value);
            }
           
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                isOK = true;
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

            return isOK;
        }
      
    }
}