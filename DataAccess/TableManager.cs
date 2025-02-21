using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataAccess
{
    public class TableManager<TObject> where TObject : class
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
        
        private static object CheckForDbnull(object value)
        {
            return value ?? DBNull.Value;
        }
        
        public static int InsertIntoTable(TObject rowInserted)
        {
            int resultID = -1;
            string table = typeof(TObject).Name;
            Type tableType = typeof(TObject);
            PropertyInfo[] prop = tableType.GetProperties();
            string columns = string.Join(",",prop.Select(x => x.Name));
            string values = string.Join(",",prop.Select(x => ToSqlTypeConvertor(x.GetValue(rowInserted))));

            try
            {
                IDbConnection dbConnection = new SqlConnection(DBSettings.connectionString);

                resultID = dbConnection.ExecuteScalar<int>(
                    DBSettings.ProceduresNames.InsertIntoAnyTable.ToString(),
                    new
                    {
                        TableName = table,
                        Columns = columns,
                        Values = values
                    },
                    commandType: CommandType.StoredProcedure
                );

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
                throw;
            }
            
            return resultID;
        }
        
        // does not work for null values i may make one that work for nulls 
        public static bool SelectFromTable(KeyValuePair<string, object> whereClauseConstruction, out List<TObject> dt)
        {
            bool isOK = false;
            string table = typeof(TObject).Name;
            string column = whereClauseConstruction.Key;

            try
            {
                IDbConnection dbConnection = new SqlConnection(DBSettings.connectionString);

                dt = dbConnection.Query<TObject>(
                    DBSettings.ProceduresNames.SelectFromTable.ToString(),
                    new
                    {
                        TableName = table,
                        ColumnName = column,
                        InputValue = CheckForDbnull(whereClauseConstruction.Value)
                    },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                isOK = dt.Count != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
                throw;
            }
            

            return isOK;
        }
        
        public static int UpdateTable(KeyValuePair<string, object> whereClause, TObject rowsUpdated)
        {
            int result = -1;
            string table = typeof(TObject).Name;
            string column = whereClause.Key;
            PropertyInfo[] prop = typeof(TObject).GetProperties();

            Dictionary<string, object> updates = new Dictionary<string, object>();
            
            foreach (PropertyInfo p in prop)
            {
                var value = p.GetValue(rowsUpdated);
                if (value != null)
                {
                    updates.Add(p.Name,value);
                }
            }

            var formattedUpdates = updates.ToDictionary(
                x => x.Key, 
                x => ToSqlTypeConvertor(x.Value)
            );
            
            JObject j_object = JObject.FromObject(formattedUpdates);
            
            string jupdates = j_object.ToString();
            
            try
            {
                IDbConnection dbConnection = new SqlConnection(DBSettings.connectionString);

                result = dbConnection.Execute(
                    DBSettings.ProceduresNames.UpdateTable.ToString(),
                    new
                    {
                        TableName = table,
                        ColumnName = column,
                        Updates = jupdates,
                        UserColumnName = CheckForDbnull(whereClause.Value)
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
                throw;
            }
            
            return result;
        }

        public static int DeleteFromTable(KeyValuePair<string, object> whereClause)
        {
            int result = -1;
            string table = typeof(TObject).Name;
            string column = whereClause.Key;

            try
            {
                IDbConnection dbConnection = new SqlConnection(DBSettings.connectionString);

                result = dbConnection.Execute(
                    DBSettings.ProceduresNames.DeleteFromTable.ToString(),
                    new
                    {
                        TableName = table,
                        ColumnName = column,
                        UserColumnInput = CheckForDbnull(whereClause.Value)
                    },
                    commandType: CommandType.StoredProcedure
                );
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
                throw;
            }
            
            return result;
        }
        
    }
}